using OK.Confix.SqlServer.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace OK.Confix.SqlServer.Repositories
{
    public class ConfigurationRepository : BaseRepository<ConfigurationEntity>
    {
        public ConfigurationRepository(DbContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<ConfigurationEntity> FindConfigurations()
        {
            return GetList();
        }

        public IEnumerable<ConfigurationEntity> FindConfigurations(int applicaionId, string name)
        {
            return GetList(x => x.ApplicationId == applicaionId && x.Name == name);
        }

        public ConfigurationEntity FindConfiguration(int id)
        {
            return GetById(id);
        }

        public ConfigurationEntity FindConfiguration(string name)
        {
            return Get(x => x.Name == name);
        }

        public ConfigurationEntity InsertConfiguration(ConfigurationEntity configuration)
        {
            Create(configuration);
            SaveChanges();

            return configuration;
        }

        public bool UpdateConfiguration(ConfigurationEntity configuration)
        {
            Edit(configuration);

            return SaveChanges() > 0;
        }

        public bool RemoveConfiguration(ConfigurationEntity configuration)
        {
            Delete(configuration);

            return SaveChanges() > 0;
        }
    }
}