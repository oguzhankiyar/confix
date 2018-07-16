using OK.Confix.SqlServer.AdoNet.DataContexts;
using OK.Confix.SqlServer.AdoNet.Helpers;
using OK.Confix.SqlServer.Core.Entities;
using OK.Confix.SqlServer.Core.Repositories;
using System;
using System.Collections.Generic;

namespace OK.Confix.SqlServer.AdoNet.Repositories
{
    internal class EnvironmentRepository : IEnvironmentRepository, IDisposable
    {
        private readonly ConfixDataContext _dataContext;

        public EnvironmentRepository(ConfixDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<EnvironmentEntity> FindEnvironments()
        {
            const string sql = @"SELECT
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate'
                                 FROM [cfx].[Environments] env (NOLOCK)
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE env.[IsDeleted] = 0x0;";

            return _dataContext.QueryList(DataReaderHelper.MapEnvironment, sql);
        }

        public IEnumerable<EnvironmentEntity> FindEnvironmentsByApplication(int applicationId)
        {
            const string sql = @"SELECT
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate'
                                 FROM [cfx].[Environments] env (NOLOCK)
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE env.[ApplicationId] = @applicationId AND env.[IsDeleted] = 0x0;";

            return _dataContext.QueryList(DataReaderHelper.MapEnvironment, sql, new { applicationId });
        }

        public EnvironmentEntity FindEnvironment(int id)
        {
            const string sql = @"SELECT
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate'
                                 FROM [cfx].[Environments] env (NOLOCK)
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE env.[Id] = @id AND env.[IsDeleted] = 0x0;";

            return _dataContext.QueryFirst(DataReaderHelper.MapEnvironment, sql, new { id });
        }

        public EnvironmentEntity FindEnvironment(string name)
        {
            const string sql = @"SELECT
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate'
                                 FROM [cfx].[Environments] env (NOLOCK)
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE env.[Name] = @name AND env.[IsDeleted] = 0x0;";

            return _dataContext.QueryFirst(DataReaderHelper.MapEnvironment, sql, new { name });
        }

        public EnvironmentEntity InsertEnvironment(EnvironmentEntity environment)
        {
            const string sql = @"INSERT INTO [cfx].[Environments]
                                    ([ApplicationId], [Name], [IsDeleted], [CreatedDate], [UpdatedDate])
                                 VALUES
                                    (@ApplicationId, @Name, @IsDeleted, @CreatedDate, @UpdatedDate);
                                 SELECT CAST(SCOPE_IDENTITY() AS INT);";

            environment.IsDeleted = false;
            environment.CreatedDate = DateTime.Now;
            environment.UpdatedDate = DateTime.Now;

            environment.Id = _dataContext.ExecuteScalar<int>(sql, new { environment.ApplicationId, environment.Name, environment.IsDeleted, environment.CreatedDate, environment.UpdatedDate });

            return environment;
        }

        public bool UpdateEnvironment(EnvironmentEntity environment)
        {
            const string sql = @"UPDATE [cfx].[Environments]
                                 SET [ApplicationId] = @ApplicationId,
                                     [Name] = @Name,
                                     [IsDeleted] = @IsDeleted,
                                     [UpdatedDate] = @UpdatedDate
                                 WHERE [Id] = @Id;
                                 SELECT @@ROWCOUNT;";

            environment.UpdatedDate = DateTime.Now;

            int rowCount = _dataContext.ExecuteScalar<int>(sql, new { environment.Id, environment.ApplicationId, environment.Name, environment.IsDeleted, environment.UpdatedDate });

            return rowCount > 0;
        }

        public bool RemoveEnvironment(EnvironmentEntity environment)
        {
            const string sql = @"DELETE FROM [cfx].[Environments]
                                 WHERE [Id] = @Id;
                                 SELECT @@ROWCOUNT;";

            int rowCount = _dataContext.ExecuteScalar<int>(sql, new { environment.Id });

            return rowCount > 0;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}