using OK.Confix.SqlServer.Core.Entities;
using System.Collections.Generic;

namespace OK.Confix.SqlServer.Core.Repositories
{
    internal interface IApplicationRepository
    {
        IEnumerable<ApplicationEntity> FindApplications();

        ApplicationEntity FindApplication(int id);

        ApplicationEntity FindApplication(string name);

        ApplicationEntity InsertApplication(ApplicationEntity application);

        bool UpdateApplication(ApplicationEntity application);

        bool RemoveApplication(ApplicationEntity application);
    }
}