namespace Dapper.Extensions.DataAccess.Sqlite.Tests;

public class ConfigurationProviderTest
{
    [Fact]
    public Task TestThrowArgumentExceptionAsync()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var host = Host.CreateDefaultBuilder();
            host.ConfigureServices((context, services) =>
            {
                services.AddSqliteDataAccess(configurationPath: "ConfigurationProviderTest.dapper.xml");
                services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
            });
            host.Build();
        });
        return Task.CompletedTask;
    }
}