using Alyio.Extensions.Dapper.Sqlite.Tests.Models;
using Alyio.Extensions.Dapper.Sqlite.Tests.Repositories;

namespace Alyio.Extensions.Dapper.Sqlite.Tests
{
    public class RepositoryTest
    {
        private IServiceProvider Services { get; }

        public RepositoryTest()
        {
            var host = Host.CreateDefaultBuilder();
            host.ConfigureServices((context, services) =>
            {
                services.AddSqliteStore(configurationPath: "RepositoryTest.dapper.xml");
                services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
                services.AddScoped<IGenreRepository, GenreRepository>();
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
            Assert.Equal("Rock", genre?.Name);

            genre!.Name = "Hello World";
            await genreRepo.UpdateAsync(genre);

            genre = await genreRepo.SelectByIdAsync(1);
            Assert.Equal("Hello World", genre?.Name);

            var genre2 = new Genre { GenreId = 26, Name = "您好世界" };
            await genreRepo.InsertAsync(genre2);
            var genre3 = await genreRepo.SelectByIdAsync(26);
            Assert.Equal(genre2.GenreId, genre3?.GenreId);
            Assert.Equal(genre2.Name, genre3?.Name);

            var artistRepo = Services.GetRequiredService<IRepository<Artist, int>>();
            var artists = await artistRepo.SelectAllAsync();
            Assert.Equal(275, artists.Count());
        }

        [Fact]
        public async Task TestCustomerThrowArgumentExceptionAsync()
        {
            var customers = Services.GetRequiredService<IRepository<Customer, int>>();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await customers.SelectAllAsync());
            Assert.Equal($"The given id '{nameof(customers.SelectAllAsync)}' was not present in the mapper.", exception.Message);
        }

        [Fact]
        public async Task TestGenreRepositorySelectByNameAsync()
        {
            var genres = Services.GetRequiredService<IGenreRepository>();
            var rock = await genres.SelectByNameAsync("Comedy");
            Assert.Equal("Comedy", rock?.Name);
        }

        [Fact]
        public async Task TestGenreRepositoryPageSelectAllAsync()
        {
            var genres = Services.GetRequiredService<IGenreRepository>();
            var (totalCount, results) = await genres.PageSelectAllAsync(-2, 10);
            Assert.Equal(10, results.Count());
            Assert.True(totalCount >= 25);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            {
                return genres.PageSelectAllAsync(0, -5);
            });

            Assert.Equal("The pageSize must be greater than zero.", exception.Message);
        }
    }
}