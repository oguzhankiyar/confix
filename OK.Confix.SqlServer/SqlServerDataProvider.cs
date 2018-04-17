using OK.Confix.Models;
using OK.Confix.SqlServer.DataContexts;
using OK.Confix.SqlServer.Entities;
using OK.Confix.SqlServer.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OK.Confix.SqlServer
{
    public class SqlServerDataProvider : IDataProvider
    {
        public SqlServerDataProviderSettings Settings;

        private ApplicationRepository applicationRepository;
        private EnvironmentRepository environmentRepository;
        private ConfigurationRepository configurationRepository;

        public SqlServerDataProvider()
        {
            Settings = new SqlServerDataProviderSettings();
        }

        internal void Build()
        {
            DbContext dataContext = new ConfixDataContext(Settings.ConnectionString);

            applicationRepository = new ApplicationRepository(dataContext);
            environmentRepository = new EnvironmentRepository(dataContext);
            configurationRepository = new ConfigurationRepository(dataContext);
        }

        #region Applications

        public List<ApplicationModel> GetApplications()
        {
            List<ApplicationEntity> applicationEntities = applicationRepository.FindApplications().ToList();

            return applicationEntities.Select(x => new ApplicationModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public ApplicationModel GetApplication(int id)
        {
            ApplicationEntity applicationEntity = applicationRepository.FindApplication(id);

            if (applicationEntity == null)
                return null;

            return new ApplicationModel()
            {
                Id = applicationEntity.Id,
                Name = applicationEntity.Name
            };
        }

        public ApplicationModel GetApplication(string name)
        {
            ApplicationEntity applicationEntity = applicationRepository.FindApplication(name);

            if (applicationEntity == null)
                return null;

            return new ApplicationModel()
            {
                Id = applicationEntity.Id,
                Name = applicationEntity.Name
            };
        }

        public bool InsertApplication(ApplicationModel application)
        {
            ApplicationEntity applicationEntity = new ApplicationEntity()
            {
                Name = application.Name
            };

            applicationRepository.InsertApplication(applicationEntity);

            return applicationEntity.Id > 0;
        }

        public bool UpdateApplication(ApplicationModel application)
        {
            ApplicationEntity applicationEntity = applicationRepository.FindApplication(application.Id);

            applicationEntity.Name = application.Name;

            return applicationRepository.UpdateApplication(applicationEntity);
        }

        public bool RemoveApplication(int id)
        {
            ApplicationEntity applicationEntity = applicationRepository.FindApplication(id);

            return applicationRepository.RemoveApplication(applicationEntity);
        }

        #endregion

        #region Environments

        public List<EnvironmentModel> GetEnvironments()
        {
            List<EnvironmentEntity> environmentEntities = environmentRepository.FindEnvironments().ToList();

            return environmentEntities.Select(x => new EnvironmentModel()
            {
                Id = x.Id,
                ApplicationId = x.ApplicationId,
                Application = new ApplicationModel()
                {
                    Id = x.Application.Id,
                    Name = x.Application.Name
                },
                Name = x.Name
            }).ToList();
        }

        public EnvironmentModel GetEnvironment(int id)
        {
            EnvironmentEntity environmentEntity = environmentRepository.FindEnvironment(id);

            if (environmentEntity == null)
                return null;

            return new EnvironmentModel()
            {
                Id = environmentEntity.Id,
                ApplicationId = environmentEntity.ApplicationId,
                Application = new ApplicationModel()
                {
                    Id = environmentEntity.Application.Id,
                    Name = environmentEntity.Application.Name
                },
                Name = environmentEntity.Name
            };
        }

        public EnvironmentModel GetEnvironment(string name)
        {
            EnvironmentEntity environmentEntity = environmentRepository.FindEnvironment(name);

            if (environmentEntity == null)
                return null;

            return new EnvironmentModel()
            {
                Id = environmentEntity.Id,
                ApplicationId = environmentEntity.ApplicationId,
                Name = environmentEntity.Name
            };
        }

        public bool InsertEnvironment(EnvironmentModel environment)
        {
            EnvironmentEntity environmentEntity = new EnvironmentEntity()
            {
                ApplicationId = environment.ApplicationId,
                Name = environment.Name
            };

            environmentRepository.InsertEnvironment(environmentEntity);

            return environmentEntity.Id > 0;
        }

        public bool UpdateEnvironment(EnvironmentModel environment)
        {
            EnvironmentEntity environmentEntity = environmentRepository.FindEnvironment(environment.Id);

            environmentEntity.ApplicationId = environment.ApplicationId;
            environmentEntity.Name = environment.Name;

            return environmentRepository.UpdateEnvironment(environmentEntity);
        }

        public bool RemoveEnvironment(int id)
        {
            EnvironmentEntity environmentEntity = environmentRepository.FindEnvironment(id);

            return environmentRepository.RemoveEnvironment(environmentEntity);
        }

        #endregion

        #region Configurations

        public List<ConfigurationModel> GetConfigurations()
        {
            List<ConfigurationEntity> configurationEntities = configurationRepository.FindConfigurations().ToList();

            return configurationEntities.Select(x => new ConfigurationModel()
            {
                Id = x.Id,
                ApplicationId = x.ApplicationId,
                Application = new ApplicationModel()
                {
                    Id = x.Application.Id,
                    Name = x.Application.Name
                },
                EnvironmentId = x.EnvironmentId,
                Environment = x.EnvironmentId == null ? null : new EnvironmentModel()
                {
                    Id = x.Environment.Id,
                    Name = x.Environment.Name
                },
                Name = x.Name,
                Value = x.Value
            }).ToList();
        }

        public List<ConfigurationModel> GetConfigurations(int applicationId, string name)
        {
            List<ConfigurationEntity> configurationEntities = configurationRepository.FindConfigurations(applicationId, name).ToList();

            return configurationEntities.Select(x => new ConfigurationModel()
            {
                Id = x.Id,
                ApplicationId = x.ApplicationId,
                Application = new ApplicationModel()
                {
                    Id = x.Application.Id,
                    Name = x.Application.Name
                },
                EnvironmentId = x.EnvironmentId,
                Environment = x.EnvironmentId == null ? null : new EnvironmentModel()
                {
                    Id = x.Environment.Id,
                    Name = x.Environment.Name
                },
                Name = x.Name,
                Value = x.Value
            }).ToList();
        }

        public ConfigurationModel GetConfiguration(int id)
        {
            ConfigurationEntity configurationEntity = configurationRepository.FindConfiguration(id);

            if (configurationEntity == null)
                return null;

            return new ConfigurationModel()
            {
                Id = configurationEntity.Id,
                ApplicationId = configurationEntity.ApplicationId,
                EnvironmentId = configurationEntity.EnvironmentId,
                Name = configurationEntity.Name,
                Value = configurationEntity.Value
            };
        }

        public ConfigurationModel GetConfiguration(int applicationId, int? environmentId, string name)
        {
            ConfigurationEntity configurationEntity = configurationRepository.FindConfiguration(name);

            if (configurationEntity == null)
                return null;

            if (configurationEntity.ApplicationId != applicationId || configurationEntity.EnvironmentId != environmentId)
                return null;

            return new ConfigurationModel()
            {
                Id = configurationEntity.Id,
                ApplicationId = configurationEntity.ApplicationId,
                Application = new ApplicationModel()
                {
                    Id = configurationEntity.Application.Id,
                    Name = configurationEntity.Application.Name
                },
                EnvironmentId = configurationEntity.EnvironmentId,
                Environment = configurationEntity.EnvironmentId == null ? null : new EnvironmentModel()
                {
                    Id = configurationEntity.Environment.Id,
                    Name = configurationEntity.Environment.Name
                },
                Name = configurationEntity.Name,
                Value = configurationEntity.Value
            };
        }

        public bool InsertConfiguration(ConfigurationModel configuration)
        {
            ConfigurationEntity configurationEntity = new ConfigurationEntity()
            {
                ApplicationId = configuration.ApplicationId,
                EnvironmentId = configuration.EnvironmentId,
                Name = configuration.Name,
                Value = configuration.Value
            };

            configurationRepository.InsertConfiguration(configurationEntity);

            return configurationEntity.Id > 0;
        }

        public bool UpdateConfiguration(ConfigurationModel configuration)
        {
            ConfigurationEntity configurationEntity = configurationRepository.FindConfiguration(configuration.Id);

            configurationEntity.ApplicationId = configuration.ApplicationId;
            configurationEntity.EnvironmentId = configuration.EnvironmentId;
            configurationEntity.Name = configuration.Name;
            configurationEntity.Value = configuration.Value;

            return configurationRepository.UpdateConfiguration(configurationEntity);
        }

        public bool RemoveConfiguration(int id)
        {
            ConfigurationEntity configurationEntity = configurationRepository.FindConfiguration(id);

            return configurationRepository.RemoveConfiguration(configurationEntity);
        }

        #endregion
    }
}