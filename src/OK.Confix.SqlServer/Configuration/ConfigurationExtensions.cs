using OK.Confix.Configuration.Core;
using OK.Confix.SqlServer.Configuration;
using OK.Confix.SqlServer.Configuration.Core;
using System;

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
            
            config.GetConfigurator().SubscribeAction(ActionType.Build, () =>
            {
                dataProvider.Build();
            });

            return config;
        }
    }
}