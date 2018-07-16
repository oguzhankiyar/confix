using OK.Confix.Configuration.Core;

namespace OK.Confix.Configuration
{
    public class WithConfigurator : IWithConfigurator
    {
        private IConfigurator _configurator;

        public WithConfigurator(IConfigurator configurator)
        {
            _configurator = configurator;
        }

        public IConfigurator GetConfigurator()
        {
            return _configurator;
        }

        public IConfixContext Build()
        {
            return _configurator.Build();
        }
    }
}