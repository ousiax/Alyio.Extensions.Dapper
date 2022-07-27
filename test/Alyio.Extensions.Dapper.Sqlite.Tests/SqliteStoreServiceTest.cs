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
                SqliteConnectionOptions options = new();
                context.Configuration.Bind(nameof(SqliteConnectionOptions), options);
                //var option = services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
                services.AddSqliteStore(opt =>
                {
                    opt.Master = options.Master;
                    opt.Slaves = options.Slaves;
                },
                "SqliteStoreServiceTest.dapper.xml");
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
        public async Task TestInsertAndUpdateAndDeleteByIdAsync()
        {
            var store = Services.GetRequiredService<IStoreService<Genre, int>>();
            var helloGenre = new Genre { GenreId = 1_024, Name = "我很好" };

            // insert
            var rowNums = await store.InsertAsync("InsertAsync", helloGenre);

            Assert.Equal(1, rowNums);

            var genre = await store.QuerySingleOrDefaultByIdAsync<Genre>("SelectByIdAsync", helloGenre.GenreId.Value);

            Assert.NotNull(genre);
            Assert.Equal(helloGenre.GenreId, genre!.GenreId);
            Assert.Equal(helloGenre.Name, genre!.Name);

            // update
            helloGenre.Name = "你怎么样？";
            rowNums = await store.UpdateAsync("UpdateAsync", helloGenre);

            Assert.Equal(1, rowNums);

            genre = await store.QuerySingleOrDefaultByIdAsync<Genre>("SelectByIdAsync", helloGenre.GenreId.Value);

            Assert.NotNull(genre);
            Assert.Equal(helloGenre.Name, genre!.Name);

            // delete
            rowNums = await store.DeleteByIdAsync("DeleteAsync", helloGenre.GenreId.Value);

            Assert.Equal(1, rowNums);

            genre = await store.QuerySingleOrDefaultByIdAsync<Genre>("SelectByIdAsync", helloGenre.GenreId.Value);

            Assert.Null(genre);
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
