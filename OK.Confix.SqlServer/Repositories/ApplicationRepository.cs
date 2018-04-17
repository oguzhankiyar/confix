using OK.Confix.SqlServer.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace OK.Confix.SqlServer.Repositories
{
    public class ApplicationRepository : BaseRepository<ApplicationEntity>
    {
        public ApplicationRepository(DbContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<ApplicationEntity> FindApplications()
        {
            return GetList();
        }

        public ApplicationEntity FindApplication(int id)
        {
            return GetById(id);
        }

        public ApplicationEntity FindApplication(string name)
        {
            return Get(x => x.Name == name);
        }

        public ApplicationEntity InsertApplication(ApplicationEntity application)
        {
            Create(application);
            SaveChanges();

            return application;
        }

        public bool UpdateApplication(ApplicationEntity application)
        {
            Edit(application);

            return SaveChanges() > 0;
        }

        public bool RemoveApplication(ApplicationEntity application)
        {
            Delete(application);

            return SaveChanges() > 0;
        }
    }
}