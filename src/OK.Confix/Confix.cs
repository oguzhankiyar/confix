using OK.Confix.Configuration;
using OK.Confix.Configuration.Core;

namespace OK.Confix
{
    public class Confix
    {
        public ConfixContext Context { get; set; }

        private Confix()
        {
            Context = new ConfixContext();
        }

        public static IConfigurator New()
        {
            Confix confix = new Confix();

            return new Configurator(confix);
        }
    }
}