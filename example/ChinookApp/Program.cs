using ChinookApp.Models;
using ChinookApp.Repositories;

using Alyio.Extensions.Dapper;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

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

Console.WriteLine("--------------");
var (pageCount, resultSet) = await genreRepository.SelectPageAsync(1, 5);
Console.WriteLine(pageCount);
foreach (var g in resultSet)
{
    Console.WriteLine(g.Name);
}