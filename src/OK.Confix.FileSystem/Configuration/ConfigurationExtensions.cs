using OK.Confix.Configuration.Core;
using OK.Confix.FileSystem.Configuration;
using OK.Confix.FileSystem.Configuration.Core;
using System;

namespace OK.Confix.FileSystem
{
    public static class ConfigurationExtensions
    {
        public static IWithConfigurator WithFileSystem(this IWithConfigurator config, Action<IFileSystemConfigurator> fileSystemConfigurator)
        {
            FileSystemDataProvider dataProvider = new FileSystemDataProvider();

            FileSystemConfigurator configurator = new FileSystemConfigurator(dataProvider);

            fileSystemConfigurator.Invoke(configurator);

            config.GetConfigurator().SetDataProvider(dataProvider);

            config.GetConfigurator().SubscribeAction(ActionType.Build, () =>
            {
                dataProvider.Build();
            });

            return config;
        }
    }
}