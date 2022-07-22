namespace Dapper.Extensions.DataAccess.Sqlite.Tests;

public class SqliteConnectionFactoryTest
{
    [Fact]
    public async Task TestOpenAndOpenReadOnlyAsync()
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices((context, services) =>
        {
            services.AddSqliteDataAccess();
            services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
        });
        var app = host.Build();
        var connectionFactory = app.Services.GetRequiredService<IConnectionFactory>();
        using var conn = await connectionFactory.OpenAsync();
        await conn.ExecuteAsync("CREATE TABLE IF NOT EXISTS SqliteConnectionFactoryTest(Id integer)");

        await Assert.ThrowsAsync<Microsoft.Data.Sqlite.SqliteException>(async () =>
        {
            using var connReadOnly = await connectionFactory.OpenAsync(OpenMode.ReadOnly);
            await connReadOnly.ExecuteAsync("DROP TABLE IF EXISTS SqliteConnectionFactoryTest");
        });
    }
}