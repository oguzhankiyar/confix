using OK.Confix.Configuration.Core;
using OK.Confix.SqlServer.Configuration;
using OK.Confix.SqlServer.Configuration.Core;
using OK.Confix.SqlServer.DataContexts;
using OK.Confix.SqlServer.Migrations;
using System;
using System.Data.Entity;

namespace OK.Confix.SqlServer
{
    public static class ConfigurationExtensions
    {
        public static IWithConfigurator WithSqlServer(this IWithConfigurator config, Action<ISqlServerConfigurator> sqlServerConfigurator)
        {
            SqlServerDataProvider dataProvider = new SqlServerDataProvider();

            SqlServerConfigurator configurator = new SqlServerConfigurator(dataProvider);

            sqlServerConfigurator.Invoke(configurator);
            
            config.GetConfigurator().SetDataProvider(dataProvider);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ConfixDataContext, MigrationConfiguration>(true));

            config.GetConfigurator().SubscribeAction(ActionType.Build, () =>
            {
                dataProvider.Build();
            });

            return config;
        }
    }
}