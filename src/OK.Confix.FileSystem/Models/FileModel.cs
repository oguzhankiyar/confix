using OK.Confix.FileSystem.Entities;
using System.Collections.Generic;

namespace OK.Confix.FileSystem.Models
{
    internal class FileModel
    {
        public List<ApplicationEntity> Applications { get; set; }

        public List<EnvironmentEntity> Environments { get; set; }

        public List<ConfigurationEntity> Configurations { get; set; }

        public FileModel()
        {
            this.Applications = new List<ApplicationEntity>();
            this.Environments = new List<EnvironmentEntity>();
            this.Configurations = new List<ConfigurationEntity>();
        }
    }
}