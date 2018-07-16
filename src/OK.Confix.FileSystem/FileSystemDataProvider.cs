using OK.Confix.FileSystem.Entities;
using OK.Confix.FileSystem.Helpers;
using OK.Confix.FileSystem.Models;
using OK.Confix.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OK.Confix.FileSystem
{
    public class FileSystemDataProvider : IDataProvider
    {
        public FileSystemDataProviderSettings Settings;

        private FileHelper _fileHelper;

        public FileSystemDataProvider()
        {
            this.Settings = new FileSystemDataProviderSettings();
        }

        internal void Build()
        {
            string filePath = $"{Settings.Path}/{Settings.FileName}.confix";

            _fileHelper = new FileHelper(filePath);
        }

        #region Applications

        public List<ApplicationModel> GetApplications()
        {
            FileModel file = _fileHelper.GetFile();

            List<ApplicationEntity> applicationEntities = file.Applications.Where(x => x.IsDeleted == false).ToList();

            return applicationEntities.Select(MapApplication).ToList();
        }

        public ApplicationModel GetApplication(int id)
        {
            FileModel file = _fileHelper.GetFile();

            ApplicationEntity applicationEntity = file.Applications.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            return MapApplication(applicationEntity);
        }

        public ApplicationModel GetApplication(string name)
        {
            FileModel file = _fileHelper.GetFile();

            ApplicationEntity applicationEntity = file.Applications.FirstOrDefault(x => x.Name == name && x.IsDeleted == false);

            return MapApplication(applicationEntity);
        }

        public bool InsertApplication(ApplicationModel application)
        {
            FileModel file = _fileHelper.GetFile();

            ApplicationEntity applicationEntity = new ApplicationEntity()
            {
                Id = file.Applications.Any() ? file.Applications.Max(x => x.Id) + 1 : 1,
                Name = application.Name,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            file.Applications.Add(applicationEntity);

            _fileHelper.SaveFile(file);

            return true;
        }

        public bool UpdateApplication(ApplicationModel application)
        {
            FileModel file = _fileHelper.GetFile();

            ApplicationEntity applicationEntity = file.Applications.FirstOrDefault(x => x.Id == application.Id);

            applicationEntity.Name = application.Name;
            applicationEntity.UpdatedDate = DateTime.Now;

            _fileHelper.SaveFile(file);

            return true;
        }

        public bool RemoveApplication(int id)
        {
            FileModel file = _fileHelper.GetFile();

            ApplicationEntity applicationEntity = file.Applications.FirstOrDefault(x => x.Id == id);

            applicationEntity.IsDeleted = true;
            applicationEntity.UpdatedDate = DateTime.Now;

            _fileHelper.SaveFile(file);

            return true;
        }

        #endregion

        #region Environments

        public List<EnvironmentModel> GetEnvironments()
        {
            FileModel file = _fileHelper.GetFile();

            List<EnvironmentEntity> environmentEntities = file.Environments.Where(x => x.IsDeleted == false).ToList();

            return environmentEntities.Select(MapEnvironment).ToList();
        }

        public List<EnvironmentModel> GetEnvironmentsByApplication(int applicationId)
        {
            FileModel file = _fileHelper.GetFile();

            List<EnvironmentEntity> environmentEntities = file.Environments.Where(x => x.ApplicationId == applicationId && x.IsDeleted == false).ToList();

            return environmentEntities.Select(MapEnvironment).ToList();
        }

        public EnvironmentModel GetEnvironment(int id)
        {
            FileModel file = _fileHelper.GetFile();

            EnvironmentEntity environmentEntity = file.Environments.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            return MapEnvironment(environmentEntity);
        }

        public EnvironmentModel GetEnvironment(string name)
        {
            FileModel file = _fileHelper.GetFile();

            EnvironmentEntity environmentEntity = file.Environments.FirstOrDefault(x => x.Name == name && x.IsDeleted == false);

            return MapEnvironment(environmentEntity);
        }

        public bool InsertEnvironment(EnvironmentModel environment)
        {
            FileModel file = _fileHelper.GetFile();

            EnvironmentEntity environmentEntity = new EnvironmentEntity()
            {
                Id = file.Environments.Any() ? file.Environments.Max(x => x.Id) + 1 : 1,
                ApplicationId = environment.ApplicationId,
                Name = environment.Name,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            file.Environments.Add(environmentEntity);

            _fileHelper.SaveFile(file);

            return true;
        }

        public bool UpdateEnvironment(EnvironmentModel environment)
        {
            FileModel file = _fileHelper.GetFile();

            EnvironmentEntity environmentEntity = file.Environments.FirstOrDefault(x => x.Id == environment.Id);

            environmentEntity.ApplicationId = environment.ApplicationId;
            environmentEntity.Name = environment.Name;
            environmentEntity.UpdatedDate = DateTime.Now;

            _fileHelper.SaveFile(file);

            return true;
        }

        public bool RemoveEnvironment(int id)
        {
            FileModel file = _fileHelper.GetFile();

            EnvironmentEntity environmentEntity = file.Environments.FirstOrDefault(x => x.Id == id);

            environmentEntity.IsDeleted = true;
            environmentEntity.UpdatedDate = DateTime.Now;

            _fileHelper.SaveFile(file);

            return true;
        }

        #endregion

        #region Configurations

        public List<ConfigurationModel> GetConfigurations()
        {
            FileModel file = _fileHelper.GetFile();

            List<ConfigurationEntity> configurationEntities = file.Configurations.Where(x => x.IsDeleted == false).ToList();

            return configurationEntities.Select(MapConfiguration).ToList();
        }

        public List<ConfigurationModel> GetConfigurations(int applicationId, string key)
        {
            FileModel file = _fileHelper.GetFile();

            List<ConfigurationEntity> configurationEntities = file.Configurations.Where(x => x.ApplicationId == applicationId && x.Name == key && x.IsDeleted == false).ToList();

            return configurationEntities.Select(MapConfiguration).ToList();
        }

        public List<ConfigurationModel> GetConfigurationsByApplication(int applicationId)
        {
            FileModel file = _fileHelper.GetFile();

            List<ConfigurationEntity> configurationEntities = file.Configurations.Where(x => x.ApplicationId == applicationId && x.IsDeleted == false).ToList();

            return configurationEntities.Select(MapConfiguration).ToList();
        }

        public List<ConfigurationModel> GetConfigurationsByEnvironment(int? environmentId)
        {
            FileModel file = _fileHelper.GetFile();

            List<ConfigurationEntity> configurationEntities = file.Configurations.Where(x => x.EnvironmentId == environmentId && x.IsDeleted == false).ToList();

            return configurationEntities.Select(MapConfiguration).ToList();
        }

        public ConfigurationModel GetConfiguration(int id)
        {
            FileModel file = _fileHelper.GetFile();

            ConfigurationEntity configurationEntity = file.Configurations.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            return MapConfiguration(configurationEntity);
        }

        public ConfigurationModel GetConfiguration(int applicationId, int? environmentId, string key)
        {
            FileModel file = _fileHelper.GetFile();

            ConfigurationEntity configurationEntity = file.Configurations.FirstOrDefault(x => x.ApplicationId == applicationId && x.EnvironmentId == environmentId && x.Name == key && x.IsDeleted == false);

            return MapConfiguration(configurationEntity);
        }

        public bool InsertConfiguration(ConfigurationModel configuration)
        {
            FileModel file = _fileHelper.GetFile();

            ConfigurationEntity configurationEntity = new ConfigurationEntity()
            {
                Id = file.Configurations.Any() ? file.Configurations.Max(x => x.Id) + 1 : 1,
                ApplicationId = configuration.ApplicationId,
                EnvironmentId = configuration.EnvironmentId,
                Name = configuration.Name,
                Type = configuration.Type,
                Value = configuration.Value,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            file.Configurations.Add(configurationEntity);

            _fileHelper.SaveFile(file);

            return true;
        }

        public bool UpdateConfiguration(ConfigurationModel configuration)
        {
            FileModel file = _fileHelper.GetFile();

            ConfigurationEntity configurationEntity = file.Configurations.FirstOrDefault(x => x.Id == configuration.Id);

            configurationEntity.ApplicationId = configuration.ApplicationId;
            configurationEntity.EnvironmentId = configuration.EnvironmentId;
            configurationEntity.Name = configuration.Name;
            configurationEntity.Value = configuration.Value;
            configurationEntity.Type = configuration.Type;
            configurationEntity.UpdatedDate = DateTime.Now;

            _fileHelper.SaveFile(file);

            return true;
        }

        public bool RemoveConfiguration(int id)
        {
            FileModel file = _fileHelper.GetFile();

            ConfigurationEntity configurationEntity = file.Configurations.FirstOrDefault(x => x.Id == id);

            configurationEntity.IsDeleted = true;
            configurationEntity.UpdatedDate = DateTime.Now;

            _fileHelper.SaveFile(file);

            return true;
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

            model.Application = GetApplication(entity.ApplicationId);

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
            model.Value = entity.Value;

            model.Application = GetApplication(entity.ApplicationId);
            model.Environment = entity.EnvironmentId.HasValue ? GetEnvironment(entity.EnvironmentId.Value) : null;

            return model;
        }

        #endregion
    }
}