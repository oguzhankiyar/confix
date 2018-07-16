using OK.Confix.Models;
using OK.Confix.SqlServer.AdoNet.DataContexts;
using OK.Confix.SqlServer.AdoNet.Helpers;
using OK.Confix.SqlServer.AdoNet.Repositories;
using OK.Confix.SqlServer.Core.Entities;
using OK.Confix.SqlServer.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OK.Confix.SqlServer
{
    public class SqlServerDataProvider : IDataProvider
    {
        public SqlServerDataProviderSettings Settings;

        private IApplicationRepository applicationRepository;
        private IEnvironmentRepository environmentRepository;
        private IConfigurationRepository configurationRepository;

        public SqlServerDataProvider()
        {
            Settings = new SqlServerDataProviderSettings();
        }

        internal void Build()
        {
            using (ConfixDataContext dataContext = new ConfixDataContext(Settings.ConnectionString))
            {
                if (Settings.IsDatabaseInitializationEnabled)
                {
                    DatabaseInitializationHelper.InitializeDatabase(dataContext);
                }

                applicationRepository = new ApplicationRepository(dataContext);
                environmentRepository = new EnvironmentRepository(dataContext);
                configurationRepository = new ConfigurationRepository(dataContext);
            }
        }

        #region Applications

        public List<ApplicationModel> GetApplications()
        {
            List<ApplicationEntity> applicationEntities = applicationRepository.FindApplications().ToList();

            return applicationEntities.Select(MapApplication).ToList();
        }

        public ApplicationModel GetApplication(int id)
        {
            ApplicationEntity applicationEntity = applicationRepository.FindApplication(id);
            
            return MapApplication(applicationEntity);
        }

        public ApplicationModel GetApplication(string name)
        {
            ApplicationEntity applicationEntity = applicationRepository.FindApplication(name);
            
            return MapApplication(applicationEntity);
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

            return environmentEntities.Select(MapEnvironment).ToList();
        }

        public List<EnvironmentModel> GetEnvironmentsByApplication(int applicationId)
        {
            List<EnvironmentEntity> environmentEntities = environmentRepository.FindEnvironmentsByApplication(applicationId).ToList();

            return environmentEntities.Select(MapEnvironment).ToList();
        }

        public EnvironmentModel GetEnvironment(int id)
        {
            EnvironmentEntity environmentEntity = environmentRepository.FindEnvironment(id);
            
            return MapEnvironment(environmentEntity);
        }

        public EnvironmentModel GetEnvironment(string name)
        {
            EnvironmentEntity environmentEntity = environmentRepository.FindEnvironment(name);
            
            return MapEnvironment(environmentEntity);
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

            return configurationEntities.Select(MapConfiguration).ToList();
        }

        public List<ConfigurationModel> GetConfigurations(int applicationId, string name)
        {
            List<ConfigurationEntity> configurationEntities = configurationRepository.FindConfigurations(applicationId, name).ToList();

            return configurationEntities.Select(MapConfiguration).ToList();
        }

        public List<ConfigurationModel> GetConfigurationsByApplication(int applicationId)
        {
            List<ConfigurationEntity> configurationEntities = configurationRepository.FindConfigurationsByApplication(applicationId).ToList();

            return configurationEntities.Select(MapConfiguration).ToList();
        }

        public List<ConfigurationModel> GetConfigurationsByEnvironment(int? environmentId)
        {
            List<ConfigurationEntity> configurationEntities = configurationRepository.FindConfigurationsByEnvironment(environmentId).ToList();

            return configurationEntities.Select(MapConfiguration).ToList();
        }

        public ConfigurationModel GetConfiguration(int id)
        {
            ConfigurationEntity configurationEntity = configurationRepository.FindConfiguration(id);
            
            return MapConfiguration(configurationEntity);
        }

        public ConfigurationModel GetConfiguration(int applicationId, int? environmentId, string name)
        {
            ConfigurationEntity configurationEntity = configurationRepository.FindConfiguration(applicationId, environmentId, name);
            
            return MapConfiguration(configurationEntity);
        }

        public bool InsertConfiguration(ConfigurationModel configuration)
        {
            ConfigurationEntity configurationEntity = new ConfigurationEntity()
            {
                ApplicationId = configuration.ApplicationId,
                EnvironmentId = configuration.EnvironmentId,
                Name = configuration.Name,
                Type = configuration.Type,
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
            configurationEntity.Type = configuration.Type;
            configurationEntity.Value = configuration.Value;

            return configurationRepository.UpdateConfiguration(configurationEntity);
        }

        public bool RemoveConfiguration(int id)
        {
            ConfigurationEntity configurationEntity = configurationRepository.FindConfiguration(id);

            return configurationRepository.RemoveConfiguration(configurationEntity);
        }

        #endregion

        #region Helpers

        private ApplicationModel MapApplication(ApplicationEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            ApplicationModel model = new ApplicationModel();

            model.Id = entity.Id;
            model.Name = entity.Name;

            return model;
        }

        private EnvironmentModel MapEnvironment(EnvironmentEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            EnvironmentModel model = new EnvironmentModel();

            model.Id = entity.Id;
            model.ApplicationId = entity.ApplicationId;
            model.Name = entity.Name;

            model.Application = MapApplication(entity.Application);

            return model;
        }

        private ConfigurationModel MapConfiguration(ConfigurationEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            ConfigurationModel model = new ConfigurationModel();

            model.Id = entity.Id;
            model.ApplicationId = entity.ApplicationId;
            model.EnvironmentId = entity.EnvironmentId;
            model.Name = entity.Name;
            model.Type = entity.Type;
            model.Value = entity.Value;

            model.Application = MapApplication(entity.Application);
            model.Environment = MapEnvironment(entity.Environment);

            return model;
        }

        #endregion
    }
}