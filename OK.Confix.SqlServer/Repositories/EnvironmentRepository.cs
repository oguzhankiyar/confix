using OK.Confix.SqlServer.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace OK.Confix.SqlServer.Repositories
{
    public class EnvironmentRepository : BaseRepository<EnvironmentEntity>
    {
        public EnvironmentRepository(DbContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<EnvironmentEntity> FindEnvironments()
        {
            return GetList();
        }

        public EnvironmentEntity FindEnvironment(int id)
        {
            return GetById(id);
        }

        public EnvironmentEntity FindEnvironment(string name)
        {
            return Get(x => x.Name == name);
        }

        public EnvironmentEntity InsertEnvironment(EnvironmentEntity environment)
        {
            Create(environment);
            SaveChanges();

            return environment;
        }

        public bool UpdateEnvironment(EnvironmentEntity environment)
        {
            Edit(environment);

            return SaveChanges() > 0;
        }

        public bool RemoveEnvironment(EnvironmentEntity environment)
        {
            Delete(environment);

            return SaveChanges() > 0;
        }
    }
}