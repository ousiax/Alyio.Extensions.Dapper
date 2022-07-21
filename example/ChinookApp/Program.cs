using ChinookApp.Models;
using ChinookApp.Repositories;

using Dapper.Extensions.DataAccess;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder();
host.ConfigureServices((context, services) =>
{
    services.AddSqliteDataAccess();
    services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
    services.AddScoped<IGenreRepository, GenreRepository>();
});
var app = host.Build();

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