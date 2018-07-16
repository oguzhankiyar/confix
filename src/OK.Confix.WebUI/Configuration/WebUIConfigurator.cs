using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using OK.Confix.WebUI.Configuration.Core;

namespace OK.Confix.WebUI.Configuration
{
    public class WebUIConfigurator : IWebUIConfigurator
    {
        private WebUIHandler _webUIHandler;

        public WebUIConfigurator(WebUIHandler webUIHandler)
        {
            _webUIHandler = webUIHandler;
        }

        public IWebUIConfigurator SetRoute(IApplicationBuilder app, string path)
        {
            path = "/" + path.Trim('/');

            app.Map(new PathString(path), (config) =>
            {
                config.Run(async (ctx) =>
                {
                    await _webUIHandler.ProcessRequestAsync(ctx);
                });
            });

            _webUIHandler.Settings.Path = path;

            return this;
        }
    }
}