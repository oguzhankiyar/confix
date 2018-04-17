using System.Web.Routing;

namespace OK.Confix.WebUI.Configuration.Core
{
    public interface IWebUIConfigurator
    {
        IWebUIConfigurator SetRoute(RouteCollection routes, string path);
    }
}