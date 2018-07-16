using OK.Confix.FileSystem;
using OK.Confix.SqlServer;
using System.IO;

namespace OK.Confix.Samples.Console
{
    public class AppInfo
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IConfixContext confix = UseConfixWithSqlServer();

            confix.Set("AppInfo", new AppInfo() { Title = "The App", Description = "The sample app with Confix" });

            AppInfo appInfo = confix.Get<AppInfo>("AppInfo");

            System.Console.WriteLine($"Hello, {appInfo.Title}!\n{appInfo.Description}");

            System.Console.ReadKey();
        }

        private static IConfixContext UseConfixWithSqlServer()
        {
            IConfixContext confix = Confix.New()
                .UseConfix((config) =>
                {
                    config.SetApplication("OK.Confix.Samples.Console")
                          .SetEnvironment("Development")
                          .UseCache(60000);
                })
                .WithSqlServer((config) =>
                {
                    config.SetConnectionString("Server=OKCOMPUTER;Database=OK.Confix;Trusted_Connection=True;MultipleActiveResultSets=True;")
                          .SetIsDatabaseInitializationEnabled(true);
                })
                .Build();

            return confix;
        }

        private static IConfixContext UseConfixWithFileSystem()
        {
            IConfixContext confix = Confix.New()
                .UseConfix((config) =>
                {
                    config.SetApplication("OK.Confix.Samples.Console")
                          .SetEnvironment("Development")
                          .UseCache(60000);
                })
                .WithFileSystem((config) =>
                {
                   config.SetPath(Directory.GetCurrentDirectory())
                         .SetFileName("app");
                })
                .Build();

            return confix;
        }
    }
}