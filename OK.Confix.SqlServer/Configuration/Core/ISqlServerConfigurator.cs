namespace OK.Confix.SqlServer.Configuration.Core
{
    public interface ISqlServerConfigurator
    {
        ISqlServerConfigurator SetConnectionString(string connectionString);
    }
}