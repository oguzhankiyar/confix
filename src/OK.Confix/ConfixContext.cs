using OK.Confix.Models;
using System;

namespace OK.Confix
{
    public class ConfixContext : IConfixContext
    {
        internal ConfixContextSettings Settings { get; set; }
        
        internal ConfixContext()
        {
            Settings = new ConfixContextSettings();
        }

        internal void Build()
        {
            if (string.IsNullOrEmpty(Settings.ApplicationName))
            {
                throw new Exception("Application is not specified.");
            }

            if (Settings.DataProvider == null)
            {
                throw new Exception("Provider is not specified.");
            }

            ApplicationModel app = Settings.DataProvider.GetApplication(Settings.ApplicationName);

            if (app == null)
            {
                app = new ApplicationModel()
                {
                    Name = Settings.ApplicationName
                };

                bool isSuccess = Settings.DataProvider.InsertApplication(app);

                if (!isSuccess)
                {
                    throw new Exception("Application cannot created!");
                }
            }

            Settings.Application = app;

            if (!string.IsNullOrEmpty(Settings.EnvironmentName))
            {
                EnvironmentModel env = Settings.DataProvider.GetEnvironment(Settings.EnvironmentName); // Use app too

                if (env == null)
                {
                    env = new EnvironmentModel()
                    {
                        ApplicationId = app.Id,
                        Name = Settings.EnvironmentName
                    };

                    bool isSuccess = Settings.DataProvider.InsertEnvironment(env);

                    if (!isSuccess)
                    {
                        throw new Exception("Environment cannot created!");
                    }
                }

                Settings.Environment = env;
            }
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }

            return Settings.DataManager.Get<T>(key);
        }

        public void Set<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            Settings.DataManager.Set(key, value);
        }
    }
}