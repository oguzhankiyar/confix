using OK.Confix.Configuration.Core;
using OK.Confix.Managers;

namespace OK.Confix.Configuration
{
    public class ConfixConfigurator : IConfixConfigurator
    {
        private Confix _confix;

        public ConfixConfigurator(Confix confix)
        {
            _confix = confix;
        }

        public IConfixConfigurator SetApplication(string name)
        {
            _confix.Context.Settings.Application = name;

            return this;
        }

        public IConfixConfigurator SetEnvironment(string name)
        {
            _confix.Context.Settings.Environment = name;

            return this;
        }

        public IConfixConfigurator UseCache(int interval = 600)
        {
            _confix.Context.Settings.CacheInterval = interval;
            _confix.Context.DataManager = new CachedDataManager(_confix.Context);

            return this;
        }
    }
}