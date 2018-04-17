using OK.Confix.WebUI.Configuration.Core;
using System.Web.Mvc;
using System.Web.Routing;

namespace OK.Confix.WebUI.Configuration
{
    public class WebUIConfigurator : IWebUIConfigurator
    {
        private WebUIHandler _webUIHandler;

        public WebUIConfigurator(WebUIHandler webUIHandler)
        {
            _webUIHandler = webUIHandler;
        }

        public IWebUIConfigurator SetRoute(RouteCollection routes, string path)
        {
            routes.Add("OK.Confix.WebUI",
                       new Route(path + "/{first}/{second}/{third}",
                       new RouteValueDictionary
                       {
                           { "first", UrlParameter.Optional },
                           { "second", UrlParameter.Optional },
                           { "third", UrlParameter.Optional }
                       },
                       new WebUIRouteHandler(_webUIHandler)));

            _webUIHandler.Settings.Path = path;

            return this;
        }
    }
}