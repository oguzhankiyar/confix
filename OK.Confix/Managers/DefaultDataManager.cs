using OK.Confix.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OK.Confix.Managers
{
    public class DefaultDataManager : IDataManager
    {
        private readonly ConfixContext _context;

        public DefaultDataManager(ConfixContext context)
        {
            _context = context;
        }

        public virtual T Get<T>(string key)
        {
            int applicationId = _context.Settings.DataProvider.GetApplication(_context.Settings.Application).Id;
            int? environmentId = string.IsNullOrEmpty(_context.Settings.Environment) ? (int?)null : _context.Settings.DataProvider.GetEnvironment(_context.Settings.Environment).Id;

            List<ConfigurationModel> configurations = _context.Settings.DataProvider.GetConfigurations(applicationId, key);

            ConfigurationModel configuration = configurations.FirstOrDefault(x => x.EnvironmentId == null);

            if (environmentId.HasValue && configurations.Any(x => x.EnvironmentId == environmentId))
            {
                configuration = configurations.FirstOrDefault(x => x.EnvironmentId == environmentId);
            }

            if (configuration == null)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(configuration.Value, typeof(T));
        }

        public virtual void Set<T>(string key, T value)
        {
            int applicationId = _context.Settings.DataProvider.GetApplication(_context.Settings.Application).Id;
            int? environmentId = string.IsNullOrEmpty(_context.Settings.Environment) ? (int?)null : _context.Settings.DataProvider.GetEnvironment(_context.Settings.Environment).Id;

            ConfigurationModel configuration = _context.Settings.DataProvider.GetConfiguration(applicationId, environmentId, key);

            if (configuration == null)
            {
                configuration = new ConfigurationModel();
                configuration.ApplicationId = applicationId;
                configuration.EnvironmentId = environmentId;
                configuration.Name = key;
                configuration.Value = value.ToString();

                _context.Settings.DataProvider.InsertConfiguration(configuration);
            }
            else
            {
                configuration.Value = value.ToString();

                _context.Settings.DataProvider.UpdateConfiguration(configuration);
            }
        }
    }
}