using OK.Confix.SqlServer.Configuration.Core;

namespace OK.Confix.SqlServer.Configuration
{
    public class SqlServerConfigurator : ISqlServerConfigurator
    {
        private SqlServerDataProvider _dataProvider;

        public SqlServerConfigurator(SqlServerDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public ISqlServerConfigurator SetConnectionString(string connectionString)
        {
            _dataProvider.Settings.ConnectionString = connectionString;

            return this;
        }

        public ISqlServerConfigurator SetIsDatabaseInitializationEnabled(bool isEnabled)
        {
            _dataProvider.Settings.IsDatabaseInitializationEnabled = isEnabled;

            return this;
        }
    }
}