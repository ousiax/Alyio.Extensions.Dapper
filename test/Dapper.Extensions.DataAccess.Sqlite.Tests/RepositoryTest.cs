using Dapper.Extensions.DataAccess.Sqlite.Tests.Models;

namespace Dapper.Extensions.DataAccess.Sqlite.Tests;

public class RepositoryTest
{
    private IServiceProvider Services { get; }

    public RepositoryTest()
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices((context, services) =>
        {
            services.AddSqliteDataAccess();
            services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
        });
        var app = host.Build();
        Services = app.Services;
        // var connectionFactory = Services.GetRequiredService<IDbConnectionFactory>();
        // using var conn = connectionFactory.OpenAsync().Result;
        // var chinookSql = File.ReadAllText("Chinook_Sqlite.sql");
        // conn.ExecuteAsync(chinookSql);
    }

    [Fact]
    public async Task TestGenreArtistCRUDAsync()
    {
        var genreRepo = Services.GetRequiredService<IRepository<Genre, int>>();
        var genres = await genreRepo.SelectAllAsync();
        Assert.Equal(25, genres.Count());

        var genre = await genreRepo.SelectByIdAsync(1);
        Assert.Equal("Rock", genre.Name);

        genre.Name = "Hello World";
        await genreRepo.UpdateAsync(genre);

        genre = await genreRepo.SelectByIdAsync(1);
        Assert.Equal("Hello World", genre.Name);

        var genre2 = new Genre { GenreId = 26, Name = "您好世界" };
        await genreRepo.InsertAsync(genre2);
        var genre3 = await genreRepo.SelectByIdAsync(26);
        Assert.Equal(genre2.GenreId, genre3.GenreId);
        Assert.Equal(genre2.Name, genre3.Name);

        var artistRepo = Services.GetRequiredService<IRepository<Artist, int>>();
        var artists = await artistRepo.SelectAllAsync();
        Assert.Equal(275, artists.Count());
    }

    [Fact]
    public async Task TestCustomerAsync()
    {
        var customers = Services.GetRequiredService<IRepository<Customer, int>>();

        await Assert.ThrowsAsync<ArgumentException>(async () => await customers.SelectAllAsync());
    }
}