namespace OK.Confix.Configuration.Core
{
    public interface IConfixConfigurator
    {
        IConfixConfigurator SetApplication(string name);

        IConfixConfigurator SetEnvironment(string name);

        IConfixConfigurator UseCache(int interval);
    }
}