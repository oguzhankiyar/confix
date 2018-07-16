using OK.Confix.SqlServer.AdoNet.DataContexts;
using OK.Confix.SqlServer.AdoNet.Helpers;
using OK.Confix.SqlServer.Core.Entities;
using OK.Confix.SqlServer.Core.Repositories;
using System;
using System.Collections.Generic;

namespace OK.Confix.SqlServer.AdoNet.Repositories
{
    internal class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ConfixDataContext _dataContext;

        public ConfigurationRepository(ConfixDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<ConfigurationEntity> FindConfigurations()
        {
            const string sql = @"SELECT
                                    conf.[Id] AS 'ConfigurationId',
                                    conf.[ApplicationId] AS 'ConfigurationApplicationId',
                                    conf.[EnvironmentId] AS 'ConfigurationEnvironmentId',
                                    conf.[Name] AS 'ConfigurationName',
                                    conf.[Type] AS 'ConfigurationType',
                                    conf.[Value] AS 'ConfigurationValue',
                                    conf.[IsDeleted] AS 'ConfigurationIsDeleted',
                                    conf.[CreatedDate] AS 'ConfigurationCreatedDate',
                                    conf.[UpdatedDate] AS 'ConfigurationUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate',
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate'
                                 FROM [cfx].[Configurations] conf (NOLOCK)
                                 INNER JOIN [cfx].[Environments] env (NOLOCK) ON env.[Id] = conf.[EnvironmentId]
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE conf.[IsDeleted] = 0x0;";

            return _dataContext.QueryList(DataReaderHelper.MapConfiguration, sql);
        }

        public IEnumerable<ConfigurationEntity> FindConfigurations(int applicationId, string name)
        {
            const string sql = @"SELECT
                                    conf.[Id] AS 'ConfigurationId',
                                    conf.[ApplicationId] AS 'ConfigurationApplicationId',
                                    conf.[EnvironmentId] AS 'ConfigurationEnvironmentId',
                                    conf.[Name] AS 'ConfigurationName',
                                    conf.[Type] AS 'ConfigurationType',
                                    conf.[Value] AS 'ConfigurationValue',
                                    conf.[IsDeleted] AS 'ConfigurationIsDeleted',
                                    conf.[CreatedDate] AS 'ConfigurationCreatedDate',
                                    conf.[UpdatedDate] AS 'ConfigurationUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate',
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate'
                                 FROM [cfx].[Configurations] conf (NOLOCK)
                                 INNER JOIN [cfx].[Environments] env (NOLOCK) ON env.[Id] = conf.[EnvironmentId]
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE conf.[ApplicationId] = @applicationId AND conf.[Name] = @name AND conf.[IsDeleted] = 0x0;";

            return _dataContext.QueryList(DataReaderHelper.MapConfiguration, sql, new { applicationId, name });
        }

        public IEnumerable<ConfigurationEntity> FindConfigurationsByApplication(int applicationId)
        {
            const string sql = @"SELECT
                                    conf.[Id] AS 'ConfigurationId',
                                    conf.[ApplicationId] AS 'ConfigurationApplicationId',
                                    conf.[EnvironmentId] AS 'ConfigurationEnvironmentId',
                                    conf.[Name] AS 'ConfigurationName',
                                    conf.[Type] AS 'ConfigurationType',
                                    conf.[Value] AS 'ConfigurationValue',
                                    conf.[IsDeleted] AS 'ConfigurationIsDeleted',
                                    conf.[CreatedDate] AS 'ConfigurationCreatedDate',
                                    conf.[UpdatedDate] AS 'ConfigurationUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate',
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate'
                                 FROM [cfx].[Configurations] conf (NOLOCK)
                                 INNER JOIN [cfx].[Environments] env (NOLOCK) ON env.[Id] = conf.[EnvironmentId]
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE conf.[ApplicationId] = @applicationId AND conf.[IsDeleted] = 0x0;";

            return _dataContext.QueryList(DataReaderHelper.MapConfiguration, sql, new { applicationId });
        }

        public IEnumerable<ConfigurationEntity> FindConfigurationsByEnvironment(int? environmentId)
        {
            const string sql = @"SELECT
                                    conf.[Id] AS 'ConfigurationId',
                                    conf.[ApplicationId] AS 'ConfigurationApplicationId',
                                    conf.[EnvironmentId] AS 'ConfigurationEnvironmentId',
                                    conf.[Name] AS 'ConfigurationName',
                                    conf.[Type] AS 'ConfigurationType',
                                    conf.[Value] AS 'ConfigurationValue',
                                    conf.[IsDeleted] AS 'ConfigurationIsDeleted',
                                    conf.[CreatedDate] AS 'ConfigurationCreatedDate',
                                    conf.[UpdatedDate] AS 'ConfigurationUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate',
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate'
                                 FROM [cfx].[Configurations] conf (NOLOCK)
                                 INNER JOIN [cfx].[Environments] env (NOLOCK) ON env.[Id] = conf.[EnvironmentId]
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE conf.[EnvironmentId] = @environmentId AND conf.[IsDeleted] = 0x0;";

            return _dataContext.QueryList(DataReaderHelper.MapConfiguration, sql, new { environmentId });
        }

        public ConfigurationEntity FindConfiguration(int id)
        {
            const string sql = @"SELECT
                                    conf.[Id] AS 'ConfigurationId',
                                    conf.[ApplicationId] AS 'ConfigurationApplicationId',
                                    conf.[EnvironmentId] AS 'ConfigurationEnvironmentId',
                                    conf.[Name] AS 'ConfigurationName',
                                    conf.[Type] AS 'ConfigurationType',
                                    conf.[Value] AS 'ConfigurationValue',
                                    conf.[IsDeleted] AS 'ConfigurationIsDeleted',
                                    conf.[CreatedDate] AS 'ConfigurationCreatedDate',
                                    conf.[UpdatedDate] AS 'ConfigurationUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate',
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate'
                                 FROM [cfx].[Configurations] conf (NOLOCK)
                                 INNER JOIN [cfx].[Environments] env (NOLOCK) ON env.[Id] = conf.[EnvironmentId]
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE conf.[Id] = @id AND conf.[IsDeleted] = 0x0";

            return _dataContext.QueryFirst(DataReaderHelper.MapConfiguration, sql, new { id });
        }

        public ConfigurationEntity FindConfiguration(int applicationId, int? environmentId, string name)
        {
            const string sql = @"SELECT
                                    conf.[Id] AS 'ConfigurationId',
                                    conf.[ApplicationId] AS 'ConfigurationApplicationId',
                                    conf.[EnvironmentId] AS 'ConfigurationEnvironmentId',
                                    conf.[Name] AS 'ConfigurationName',
                                    conf.[Type] AS 'ConfigurationType',
                                    conf.[Value] AS 'ConfigurationValue',
                                    conf.[IsDeleted] AS 'ConfigurationIsDeleted',
                                    conf.[CreatedDate] AS 'ConfigurationCreatedDate',
                                    conf.[UpdatedDate] AS 'ConfigurationUpdatedDate',
                                    app.[Id] AS 'ApplicationId',
                                    app.[Name] AS 'ApplicationName',
                                    app.[IsDeleted] AS 'ApplicationIsDeleted',
                                    app.[CreatedDate] AS 'ApplicationCreatedDate',
                                    app.[UpdatedDate] AS 'ApplicationUpdatedDate',
                                    env.[Id] AS 'EnvironmentId',
                                    env.[ApplicationId] AS 'EnvironmentApplicationId',
                                    env.[Name] AS 'EnvironmentName',
                                    env.[IsDeleted] AS 'EnvironmentIsDeleted',
                                    env.[CreatedDate] AS 'EnvironmentCreatedDate',
                                    env.[UpdatedDate] AS 'EnvironmentUpdatedDate'
                                 FROM [cfx].[Configurations] conf (NOLOCK)
                                 INNER JOIN [cfx].[Environments] env (NOLOCK) ON env.[Id] = conf.[EnvironmentId]
                                 INNER JOIN [cfx].[Applications] app (NOLOCK) ON app.[Id] = env.[ApplicationId]
                                 WHERE conf.[ApplicationId] = @applicationId AND ((@environmentId IS NULL AND conf.[EnvironmentId] IS NULL) OR conf.[EnvironmentId] = @environmentId) AND conf.[Name] = @name AND conf.[IsDeleted] = 0x0";

            return _dataContext.QueryFirst(DataReaderHelper.MapConfiguration, sql, new { applicationId, environmentId, name });
        }

        public ConfigurationEntity InsertConfiguration(ConfigurationEntity configuration)
        {
            const string sql = @"INSERT INTO [cfx].[Configurations]
                                    ([ApplicationId], [EnvironmentId], [Name], [Type], [Value], [IsDeleted], [CreatedDate], [UpdatedDate])
                                 VALUES
                                    (@ApplicationId, @EnvironmentId, @Name, @Type, @Value, @IsDeleted, @CreatedDate, @UpdatedDate);
                                 SELECT CAST(SCOPE_IDENTITY() AS INT);";

            configuration.IsDeleted = false;
            configuration.CreatedDate = DateTime.Now;
            configuration.UpdatedDate = DateTime.Now;

            configuration.Id = _dataContext.ExecuteScalar<int>(sql, new { configuration.ApplicationId, configuration.EnvironmentId, configuration.Name, configuration.Type, configuration.Value, configuration.IsDeleted, configuration.CreatedDate, configuration.UpdatedDate });

            return configuration;
        }

        public bool UpdateConfiguration(ConfigurationEntity configuration)
        {
            const string sql = @"UPDATE [cfx].[Configurations]
                                 SET [ApplicationId] = @ApplicationId,
                                     [EnvironmentId] = @EnvironmentId,
                                     [Name] = @Name,
                                     [Type] = @Type,
                                     [Value] = @Value,
                                     [IsDeleted] = @IsDeleted,
                                     [UpdatedDate] = @UpdatedDate
                                 WHERE [Id] = @Id;
                                 SELECT @@ROWCOUNT;";

            configuration.UpdatedDate = DateTime.Now;

            int rowCount = _dataContext.ExecuteScalar<int>(sql, new { configuration.Id, configuration.ApplicationId, configuration.EnvironmentId, configuration.Name, configuration.Type, configuration.Value, configuration.IsDeleted, configuration.UpdatedDate });

            return rowCount > 0;
        }

        public bool RemoveConfiguration(ConfigurationEntity configuration)
        {
            const string sql = @"DELETE FROM [cfx].[Configurations]
                                 WHERE [Id] = @Id;
                                 SELECT @@ROWCOUNT;";

            int rowCount = _dataContext.ExecuteScalar<int>(sql, new { configuration.Id });

            return rowCount > 0;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}