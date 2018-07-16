using OK.Confix.Helpers;
using OK.Confix.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OK.Confix.Managers
{
    internal class DefaultDataManager : IDataManager
    {
        private readonly ConfixContextSettings _settings;

        public DefaultDataManager(ConfixContextSettings settings)
        {
            _settings = settings;
        }

        public virtual T Get<T>(string key)
        {
            List<ConfigurationModel> configurations = _settings.DataProvider.GetConfigurations(_settings.Application.Id, key);

            ConfigurationModel configuration = configurations.FirstOrDefault(x => x.EnvironmentId == null);

            if (_settings.Environment != null && configurations.Any(x => x.EnvironmentId == _settings.Environment.Id))
            {
                configuration = configurations.FirstOrDefault(x => x.EnvironmentId == _settings.Environment.Id);
            }

            if (configuration == null)
            {
                return default(T);
            }

            T value = JsonHelper.Deserialize<T>(configuration.Value);

            return value;
        }

        public virtual void Set<T>(string key, T value)
        {
            ConfigurationModel configuration = _settings.DataProvider.GetConfiguration(_settings.Application.Id, _settings.Environment?.Id, key);

            if (configuration == null)
            {
                CreateConfiguration(_settings.Application.Id, _settings.Environment?.Id, key, value);
            }
            else
            {
                EditConfiguration(configuration.Id, configuration.ApplicationId, configuration.EnvironmentId, configuration.Name, value);
            }
        }

        #region Applications


        public List<ApplicationModel> GetApplications()
        {
            return _settings.DataProvider.GetApplications();
        }

        public ApplicationModel GetApplication(int id)
        {
            if (id == 0)
            {
                throw new Exception("The application id is not specified.");
            }

            return _settings.DataProvider.GetApplication(id);
        }

        public bool CreateApplication(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("The application name is not specified.");
            }

            ApplicationModel app = _settings.DataProvider.GetApplication(name);

            if (app != null)
            {
                throw new Exception("The application name is created already.");
            }

            app = new ApplicationModel()
            {
                Name = name
            };

            return _settings.DataProvider.InsertApplication(app);
        }

        public bool EditApplication(int id, string name)
        {
            if (id == 0)
            {
                throw new Exception("The application id is not specified.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("The application name is not specified.");
            }

            ApplicationModel app = _settings.DataProvider.GetApplication(name);

            if (app != null && app.Id != id)
            {
                throw new Exception("The application name is created already.");
            }

            app = new ApplicationModel()
            {
                Id = id,
                Name = name
            };

            return _settings.DataProvider.UpdateApplication(app);
        }

        public bool DeleteApplication(int id)
        {
            if (id == 0)
            {
                throw new Exception("The application id is not specified.");
            }

            List<EnvironmentModel> envs = _settings.DataProvider.GetEnvironmentsByApplication(id);

            if (envs.Any())
            {
                throw new Exception("The application has some environments. Please delete environments firstly.");
            }

            List<ConfigurationModel> confs = _settings.DataProvider.GetConfigurationsByApplication(id);

            if (confs.Any())
            {
                throw new Exception("The application has some configurations. Please delete configurations firstly.");
            }

            return _settings.DataProvider.RemoveApplication(id);
        }

        #endregion

        #region Environments

        public List<EnvironmentModel> GetEnvironments()
        {
            return _settings.DataProvider.GetEnvironments();
        }

        public EnvironmentModel GetEnvironment(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _settings.DataProvider.GetEnvironment(id);
        }

        public bool CreateEnvironment(int applicationId, string name)
        {
            if (applicationId == 0)
            {
                throw new Exception("The environment application id is not specified.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("The environment name is not specified.");
            }

            EnvironmentModel env = _settings.DataProvider.GetEnvironment(name);

            if (env != null && env.ApplicationId == applicationId)
            {
                throw new Exception("The environment name is created in the application already.");
            }

            env = new EnvironmentModel()
            {
                ApplicationId = applicationId,
                Name = name
            };

            return _settings.DataProvider.InsertEnvironment(env);
        }

        public bool EditEnvironment(int id, int applicationId, string name)
        {
            if (id == 0)
            {
                throw new Exception("The environment id is not specified.");
            }

            if (applicationId == 0)
            {
                throw new Exception("The environment application id is not specified.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("The environment name is not specified.");
            }

            EnvironmentModel env = _settings.DataProvider.GetEnvironment(name);

            if (env != null && env.ApplicationId == applicationId && env.Id != id)
            {
                throw new Exception("The environment name is created in the application already.");
            }

            env = new EnvironmentModel()
            {
                Id = id,
                ApplicationId = applicationId,
                Name = name
            };

            return _settings.DataProvider.UpdateEnvironment(env);
        }

        public bool DeleteEnvironment(int id)
        {
            if (id == 0)
            {
                throw new Exception("The environment id is not specified.");
            }

            List<ConfigurationModel> confs = _settings.DataProvider.GetConfigurationsByEnvironment(id);

            if (confs.Any())
            {
                throw new Exception("The application has some configurations. Please delete configurations firstly.");
            }

            return _settings.DataProvider.RemoveEnvironment(id);
        }

        #endregion

        #region Configurations

        public List<ConfigurationModel> GetConfigurations()
        {
            return _settings.DataProvider.GetConfigurations();
        }

        public ConfigurationModel GetConfiguration(int id)
        {
            if (id == 0)
            {
                throw new Exception("The configuration id is not specified.");
            }

            return _settings.DataProvider.GetConfiguration(id);
        }

        public bool CreateConfiguration(int applicationId, int? environmentId, string name, object value)
        {
            if (applicationId == 0)
            {
                throw new Exception("The configuration application id is not specified.");
            }

            if (environmentId == null || environmentId == 0)
            {
                throw new Exception("The configuration environment id is not specified.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("The configuration name is not specified.");
            }

            ConfigurationModel conf = _settings.DataProvider.GetConfiguration(applicationId, environmentId, name);

            if (conf != null)
            {
                throw new Exception("The environment name is created in the application and environment already.");
            }

            conf = new ConfigurationModel()
            {
                ApplicationId = applicationId,
                EnvironmentId = environmentId,
                Name = name,
                Type = GetObjectType(value),
                Value = JsonHelper.Serialize(value)
            };

            return _settings.DataProvider.InsertConfiguration(conf);
        }

        public bool EditConfiguration(int id, int applicationId, int? environmentId, string name, object value)
        {
            if (applicationId == 0)
            {
                throw new Exception("The configuration application id is not specified.");
            }

            if (environmentId == null || environmentId == 0)
            {
                throw new Exception("The configuration environment id is not specified.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("The configuration name is not specified.");
            }

            ConfigurationModel conf = _settings.DataProvider.GetConfiguration(applicationId, environmentId, name);

            if (conf != null && conf.Id != id)
            {
                throw new Exception("The environment name is created in the application and environment already.");
            }

            conf = new ConfigurationModel()
            {
                Id = id,
                ApplicationId = applicationId,
                EnvironmentId = environmentId,
                Name = name,
                Type = GetObjectType(value),
                Value = JsonHelper.Serialize(value)
            };

            return _settings.DataProvider.UpdateConfiguration(conf);
        }

        public bool DeleteConfiguration(int id)
        {
            if (id == 0)
            {
                throw new Exception("The configuration id is not specified.");
            }

            return _settings.DataProvider.RemoveConfiguration(id);
        }

        #endregion

        #region Helpers

        private string GetObjectType(object value)
        {
            if (value == null)
            {
                return "Object";
            }

            Type valueType = value.GetType();

            if (valueType == typeof(string))
            {
                return "String";
            }
            else if (valueType == typeof(int))
            {
                return "Integer";
            }
            else if (valueType == typeof(decimal))
            {
                return "Decimal";
            }
            else if (valueType == typeof(bool))
            {
                return "Boolean";
            }
            else if (valueType == typeof(DateTime))
            {
                return "DateTime";
            }
            else
            {
                return "Object";
            }
        }

        #endregion
    }
}