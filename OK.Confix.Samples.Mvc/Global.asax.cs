using OK.Confix.FileSystem;
using OK.Confix.Samples.Mvc.Helpers;
using OK.Confix.SqlServer;
using OK.Confix.WebUI;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OK.Confix.Samples.Mvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            UseFileSystemProvidedConfix();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void UseSqlServerProvidedConfix()
        {
            ConfigurationHelper.Confix = Confix.New()
                                               .UseConfix((config) =>
                                               {
                                                   config.SetApplication("OK.Confix")
                                                         .SetEnvironment("Production")
                                                         .UseCache(60);
                                               })
                                               .WithSqlServer((config) =>
                                               {
                                                   config.SetConnectionString("Data Source=OKCOMPUTER; Initial Catalog=OK.Confix; Integrated Security=True; MultipleActiveResultSets=True;");
                                               })
                                               .WithWebUI((config) =>
                                               {
                                                   config.SetRoute(RouteTable.Routes, "confix");
                                               })
                                               .Build();
        }

        private void UseFileSystemProvidedConfix()
        {
            ConfigurationHelper.Confix = Confix.New()
                                               .UseConfix((config) =>
                                               {
                                                   config.SetApplication("OK.Confix")
                                                         .SetEnvironment("Production")
                                                         .UseCache(60);
                                               })
                                               .WithFileSystem((config) =>
                                               {
                                                   config.SetPath(Server.MapPath("~/"))
                                                         .SetFileName("app");
                                               })
                                               .WithWebUI((config) =>
                                               {
                                                   config.SetRoute(RouteTable.Routes, "confix");
                                               })
                                               .Build();
        }
    }
}