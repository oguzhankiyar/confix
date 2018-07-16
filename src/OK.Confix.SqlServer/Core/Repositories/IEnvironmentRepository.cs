using OK.Confix.SqlServer.Core.Entities;
using System.Collections.Generic;

namespace OK.Confix.SqlServer.Core.Repositories
{
    internal interface IEnvironmentRepository
    {
        IEnumerable<EnvironmentEntity> FindEnvironments();

        IEnumerable<EnvironmentEntity> FindEnvironmentsByApplication(int applicationId);

        EnvironmentEntity FindEnvironment(int id);

        EnvironmentEntity FindEnvironment(string name);

        EnvironmentEntity InsertEnvironment(EnvironmentEntity environment);

        bool UpdateEnvironment(EnvironmentEntity environment);

        bool RemoveEnvironment(EnvironmentEntity environment);
    }
}