# Alyio.Extensions.Dapper

![Build Status](https://github.com/qqbuby/Alyio.Extensions.Dapper/actions/workflows/ci.yml/badge.svg?branch=main)

```cs
using ChinookApp.Models;
using ChinookApp.Repositories;

using Alyio.Extensions.Dapper;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector.Logging;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder();

// Configure MySqlConnectionOption from user secrets.
host.ConfigureAppConfiguration(builder => builder.AddUserSecrets(typeof(Program).Assembly, false));

host.ConfigureServices((context, services) =>
{
    services.AddMySqlDataAccess();
    services.Configure<MySqlConnectionOptions>(context.Configuration.GetSection(nameof(MySqlConnectionOptions)));
    services.AddScoped<IGenreRepository, GenreRepository>();
});
var app = host.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
MySqlConnectorLogManager.Provider = new MicrosoftExtensionsLoggingLoggerProvider(loggerFactory);

var genericRepository = app.Services.GetRequiredService<IRepository<Genre, int>>();
var genres = await genericRepository.SelectAllAsync();
foreach (var artist in genres.Take(10))
{
    Console.WriteLine(artist.Name);
}

Console.WriteLine("--------------");
var genreRepository = app.Services.GetRequiredService<IGenreRepository>();
var genres2 = await genreRepository.SelectAllAsync();
foreach (var artist in genres.Take(10))
{
    Console.WriteLine(artist.Name);
}

Console.WriteLine("--------------");
var genre3 = await genreRepository.SelectByNameAsync(genres2.First()!.Name!);
Console.WriteLine(genre3.Name);
```

```console
$ dotnet run
info: MySqlConnector.ConnectionPool[0]
      Pool1 creating new connection pool for ConnectionString: Server=local.io;Port=53306;User ID=root;Database=Chinook;Pooling=True;Minimum Pool Size=64;Maximum Pool Size=512
Rock
Jazz
Metal
Alternative & Punk
Rock And Roll
Blues
Latin
Reggae
Pop
Soundtrack
--------------
Rock
Jazz
Metal
Alternative & Punk
Rock And Roll
Blues
Latin
Reggae
Pop
Soundtrack
--------------
Rock
info: MySqlConnector.ConnectionPool[0]
      Pool1 clearing connection pool
```
