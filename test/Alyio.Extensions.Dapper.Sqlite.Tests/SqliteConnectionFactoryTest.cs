using Dapper;

namespace Alyio.Extensions.Dapper.Sqlite.Tests
{
    public class SqliteConnectionFactoryTest
    {
        [Fact]
        public async Task TestOpenAndOpenReadOnlyAsync()
        {
            var host = Host.CreateDefaultBuilder();
            host.ConfigureServices((context, services) =>
            {
                services.AddSqliteStore(configurationPath: "SqliteConnectionFactoryTest.dapper.xml");
                services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
            });
            var app = host.Build();
            var connectionFactory = app.Services.GetRequiredService<IConnectionFactory>();
            using var conn = await connectionFactory.OpenAsync();
            await conn.ExecuteAsync("CREATE TABLE IF NOT EXISTS SqliteConnectionFactoryTest(Id integer)");

            var exception = await Assert.ThrowsAsync<Microsoft.Data.Sqlite.SqliteException>(async () =>
            {
                using var connReadOnly = await connectionFactory.OpenAsync(OpenMode.ReadOnly);
                await connReadOnly.ExecuteAsync("DROP TABLE IF EXISTS SqliteConnectionFactoryTest");
            });

            Assert.Contains("attempt to write a readonly database", exception.Message);
        }
    }
}