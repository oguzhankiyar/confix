using OK.Confix.Configuration.Core;
using OK.Confix.WebUI.Configuration;
using OK.Confix.WebUI.Configuration.Core;
using System;

namespace OK.Confix.WebUI
{
    public static class ConfigurationExtensions
    {
        public static IWithConfigurator WithWebUI(this IWithConfigurator config, Action<IWebUIConfigurator> webUIConfigurator)
        {
            WebUIHandler webUIHandler = new WebUIHandler();

            webUIHandler.Settings.DataManager = config.GetConfigurator().GetDataManager();

            WebUIConfigurator configurator = new WebUIConfigurator(webUIHandler);

            webUIConfigurator.Invoke(configurator);

            config.GetConfigurator().SubscribeAction(ActionType.Build, () =>
            {
                webUIHandler.Build();
            });

            return config;
        }
    }
}
