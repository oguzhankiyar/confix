namespace OK.Confix.Configuration.Core
{
    public interface IWithConfigurator
    {
        IConfigurator GetConfigurator();

        IConfixContext Build();
    }
}