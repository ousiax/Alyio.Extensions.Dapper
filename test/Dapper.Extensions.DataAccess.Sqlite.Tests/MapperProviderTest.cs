using Dapper.Extensions.DataAccess.Sqlite.Tests.Models;

namespace Dapper.Extensions.DataAccess.Sqlite.Tests;

public class MapperProviderTest
{
    [Fact]
    public Task TestThrowArgumentExceptionAsync()
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices((context, services) =>
        {
            services.AddSqliteDataAccess(configurationPath: "MapperProviderTest.dapper.xml");
            services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
        });
        var app = host.Build();

        Assert.Throws<ArgumentException>(() => app.Services.GetRequiredService<IMapperProvider<Album, int>>());
        return Task.CompletedTask;
    }
}