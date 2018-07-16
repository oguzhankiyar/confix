using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OK.Confix.FileSystem;
using OK.Confix.SqlServer;
using OK.Confix.WebUI;

namespace OK.Confix.Samples.Web
{
    public class AppInfo
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            IConfixContext confix = UseConfixWithSqlServer(app, env);

            confix.Set("AppInfo", new AppInfo() { Title = "The App", Description = "The sample app with Confix" });

            app.Run(async (context) =>
            {
                AppInfo appInfo = confix.Get<AppInfo>("AppInfo");
                
                await context.Response.WriteAsync($"Hello, {appInfo.Title}!\n{appInfo.Description}");
            });
        }

        private IConfixContext UseConfixWithSqlServer(IApplicationBuilder app, IHostingEnvironment env)
        {
            IConfixContext confix = Confix.New()
                .UseConfix((config) =>
                {
                    config.SetApplication("OK.Confix.Samples.Web")
                          .SetEnvironment("Development")
                          .UseCache(60000);
                })
                .WithSqlServer((config) =>
                {
                    config.SetConnectionString("Server=OKCOMPUTER;Database=OK.Confix;Trusted_Connection=True;MultipleActiveResultSets=True;")
                          .SetIsDatabaseInitializationEnabled(true);
                })
                .WithWebUI((config) =>
                {
                    config.SetRoute(app, "confix");
                })
                .Build();

            return confix;
        }
        
        private IConfixContext UseConfixWithFileSystem(IApplicationBuilder app, IHostingEnvironment env)
        {
            IConfixContext confix = Confix.New()
                .UseConfix((config) =>
                {
                    config.SetApplication("OK.Confix.Samples.Web")
                          .SetEnvironment("Development")
                          .UseCache(60000);
                })
                .WithFileSystem((config) =>
                {
                   config.SetPath(env.WebRootPath)
                         .SetFileName("app");
                })
                .WithWebUI((config) =>
                {
                    config.SetRoute(app, "confix");
                })
                .Build();

            return confix;
        }
    }
}