using Alyio.Extensions.Dapper.Sqlite.Tests.Models;

namespace Alyio.Extensions.Dapper.Sqlite.Tests
{
    public class SqliteStoreServiceTest
    {
        private IServiceProvider Services { get; }

        public SqliteStoreServiceTest()
        {
            var host = Host.CreateDefaultBuilder();
            host.ConfigureServices((context, services) =>
            {
                services.AddSqliteStore(configurationPath: "SqliteStoreServiceTest.dapper.xml");
                services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
            });
            var app = host.Build();
            Services = app.Services;
        }

        [Fact]
        public async Task TestQuerySingleOrDefaultByIdAsync()
        {
            var store = Services.GetRequiredService<IStoreService<Genre, int>>();

            var genre1 = await store.QuerySingleOrDefaultByIdAsync<Genre>("SelectByIdAsync", 1);

            Assert.Equal(1, genre1!.GenreId);

            var genre2 = await store.QuerySingleOrDefaultByIdAsync<Genre>("SelectByIdAsync", -1);

            Assert.Null(genre2);
        }

        [Fact]
        public async Task TestPageQueryAsync()
        {
            var store = Services.GetRequiredService<IStoreService<Genre, int>>();
            var (totalCount1, results1) = await store.PageQueryAsync<Genre>("PageSelectAsync", -1, 10);
            var (totalCount2, results2) = await store.PageQueryAsync<Genre>("PageSelectAsync", 0, 10);

            Assert.Equal(totalCount1, totalCount2);
            Assert.Equal(results1.Count(), results2.Count());

            var argumentException = await Assert.ThrowsAsync<ArgumentException>(() => store.PageQueryAsync<Genre>("PageSelectAsync", 0, -1));
            Assert.Contains("must be greater than zero.", argumentException.Message);

            var invalidCastException = await Assert.ThrowsAsync<InvalidCastException>(() => store.PageQueryAsync<Genre>("InvalidOpeartionPageSelectAsync", 0, 1));
            Assert.Contains("must be CommandType.Text.", invalidCastException.Message);
        }
    }
}
