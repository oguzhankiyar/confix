using Microsoft.AspNetCore.Builder;

namespace OK.Confix.WebUI.Configuration.Core
{
    public interface IWebUIConfigurator
    {
        IWebUIConfigurator SetRoute(IApplicationBuilder routes, string path);
    }
}