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

            return applicationEntities.Select(x => new ApplicationModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public ApplicationModel GetApplication(int id)
        {
            FileModel file = _fileHelper.GetFile();

            ApplicationEntity applicationEntity = file.Applications.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            return new ApplicationModel()
            {
                Id = applicationEntity.Id,
                Name = applicationEntity.Name
            };
        }

        public ApplicationModel GetApplication(string name)
        {
            FileModel file = _fileHelper.GetFile();

            ApplicationEntity applicationEntity = file.Applications.FirstOrDefault(x => x.Name == name && x.IsDeleted == false);

            return new ApplicationModel()
            {
                Id = applicationEntity.Id,
                Name = applicationEntity.Name
            };
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

            return environmentEntities.Select(x => new EnvironmentModel()
            {
                Id = x.Id,
                ApplicationId = x.ApplicationId,
                Application = GetApplication(x.ApplicationId),
                Name = x.Name
            }).ToList();
        }

        public EnvironmentModel GetEnvironment(int id)
        {
            FileModel file = _fileHelper.GetFile();

            EnvironmentEntity environmentEntity = file.Environments.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            return new EnvironmentModel()
            {
                Id = environmentEntity.Id,
                ApplicationId = environmentEntity.ApplicationId,
                Application = GetApplication(environmentEntity.ApplicationId),
                Name = environmentEntity.Name
            };
        }

        public EnvironmentModel GetEnvironment(string name)
        {
            FileModel file = _fileHelper.GetFile();

            EnvironmentEntity environmentEntity = file.Environments.FirstOrDefault(x => x.Name == name && x.IsDeleted == false);

            return new EnvironmentModel()
            {
                Id = environmentEntity.Id,
                ApplicationId = environmentEntity.ApplicationId,
                Application = GetApplication(environmentEntity.ApplicationId),
                Name = environmentEntity.Name
            };
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

            return configurationEntities.Select(x => new ConfigurationModel()
            {
                Id = x.Id,
                ApplicationId = x.ApplicationId,
                Application = GetApplication(x.ApplicationId),
                EnvironmentId = x.EnvironmentId,
                Environment = x.EnvironmentId.HasValue ? GetEnvironment(x.EnvironmentId.Value) : null,
                Name = x.Name,
                Value = x.Value
            }).ToList();
        }

        public List<ConfigurationModel> GetConfigurations(int applicationId, string key)
        {
            FileModel file = _fileHelper.GetFile();

            List<ConfigurationEntity> configurationEntities = file.Configurations.Where(x => x.ApplicationId == applicationId && x.Name == key && x.IsDeleted == false).ToList();

            return configurationEntities.Select(x => new ConfigurationModel()
            {
                Id = x.Id,
                ApplicationId = x.ApplicationId,
                Application = GetApplication(x.ApplicationId),
                EnvironmentId = x.EnvironmentId,
                Environment = x.EnvironmentId.HasValue ? GetEnvironment(x.EnvironmentId.Value) : null,
                Name = x.Name,
                Value = x.Value
            }).ToList();
        }

        public ConfigurationModel GetConfiguration(int id)
        {
            FileModel file = _fileHelper.GetFile();

            ConfigurationEntity configurationEntity = file.Configurations.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            return new ConfigurationModel()
            {
                Id = configurationEntity.Id,
                ApplicationId = configurationEntity.ApplicationId,
                Application = GetApplication(configurationEntity.ApplicationId),
                EnvironmentId = configurationEntity.EnvironmentId,
                Environment = configurationEntity.EnvironmentId.HasValue ? GetEnvironment(configurationEntity.EnvironmentId.Value) : null,
                Name = configurationEntity.Name,
                Value = configurationEntity.Value
            };
        }

        public ConfigurationModel GetConfiguration(int applicationId, int? environmentId, string key)
        {
            FileModel file = _fileHelper.GetFile();

            ConfigurationEntity configurationEntity = file.Configurations.FirstOrDefault(x => x.ApplicationId == applicationId && x.EnvironmentId == environmentId && x.Name == key && x.IsDeleted == false);

            return new ConfigurationModel()
            {
                Id = configurationEntity.Id,
                ApplicationId = configurationEntity.ApplicationId,
                Application = GetApplication(configurationEntity.ApplicationId),
                EnvironmentId = configurationEntity.EnvironmentId,
                Environment = configurationEntity.EnvironmentId.HasValue ? GetEnvironment(configurationEntity.EnvironmentId.Value) : null,
                Name = configurationEntity.Name,
                Value = configurationEntity.Value
            };
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
    }
}