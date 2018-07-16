# OK.Confix


A Simple Configuration Management Library for .Net

## Setup

Setup Using Sql Server

```c#
using OK.Confix;
using OK.Confix.SqlServer;
using OK.Confix.WebUI;

...

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
```

Setup Using File System

```c#
using OK.Confix;
using OK.Confix.FileSystem;
using OK.Confix.WebUI;

...

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
```

## Usage


Set Value

```c#
confix.Set("AppName", "The App");
```

Get Value

```c#
string appName = confix.Get<string>("The App");
```