using OK.Confix.SqlServer.AdoNet.DataContexts;
using OK.Confix.SqlServer.AdoNet.Helpers;
using OK.Confix.SqlServer.Core.Entities;
using OK.Confix.SqlServer.Core.Repositories;
using System;
using System.Collections.Generic;

namespace OK.Confix.SqlServer.AdoNet.Repositories
{
    internal class ApplicationRepository : IApplicationRepository, IDisposable
    {
        private readonly ConfixDataContext _dataContext;

        public ApplicationRepository(ConfixDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<ApplicationEntity> FindApplications()
        {
            const string sql = @"SELECT
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate'
                                 FROM [cfx].[Applications] app (NOLOCK)
                                 WHERE app.[IsDeleted] = 0x0;";

            return _dataContext.QueryList(DataReaderHelper.MapApplication, sql);
        }

        public ApplicationEntity FindApplication(int id)
        {
            const string sql = @"SELECT
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate'
                                 FROM [cfx].[Applications] app (NOLOCK)
                                 WHERE app.[Id] = @id AND app.[IsDeleted] = 0x0;";

            return _dataContext.QueryFirst(DataReaderHelper.MapApplication, sql, new { id });
        }

        public ApplicationEntity FindApplication(string name)
        {
            const string sql = @"SELECT
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate'
                                 FROM [cfx].[Applications] app (NOLOCK)
                                 WHERE app.[Name] = @name AND app.[IsDeleted] = 0x0;";

            return _dataContext.QueryFirst(DataReaderHelper.MapApplication, sql, new { name });
        }

        public ApplicationEntity InsertApplication(ApplicationEntity application)
        {
            const string sql = @"INSERT INTO [cfx].[Applications]
                                    ([Name], [IsDeleted], [CreatedDate], [UpdatedDate])
                                 VALUES
                                    (@Name, @IsDeleted, @CreatedDate, @UpdatedDate);
                                 SELECT CAST(SCOPE_IDENTITY() AS INT);";

            application.IsDeleted = false;
            application.CreatedDate = DateTime.Now;
            application.UpdatedDate = DateTime.Now;

            application.Id = _dataContext.ExecuteScalar<int>(sql, new { application.Name, application.IsDeleted, application.CreatedDate, application.UpdatedDate });

            return application;
        }

        public bool UpdateApplication(ApplicationEntity application)
        {
            const string sql = @"UPDATE [cfx].[Applications]
                                 SET [Name] = @Name,
                                     [IsDeleted] = @IsDeleted,
                                     [UpdatedDate] = @UpdatedDate
                                 WHERE [Id] = @Id;
                                 SELECT @@ROWCOUNT;";

            application.UpdatedDate = DateTime.Now;

            int rowCount = _dataContext.ExecuteScalar<int>(sql, new { application.Id, application.Name, application.IsDeleted, application.UpdatedDate });

            return rowCount > 0;
        }

        public bool RemoveApplication(ApplicationEntity application)
        {
            const string sql = @"DELETE FROM [cfx].[Applications]
                                 WHERE [Id] = @Id;
                                 SELECT @@ROWCOUNT;";

            int rowCount = _dataContext.ExecuteScalar<int>(sql, new { application.Id });

            return rowCount > 0;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}