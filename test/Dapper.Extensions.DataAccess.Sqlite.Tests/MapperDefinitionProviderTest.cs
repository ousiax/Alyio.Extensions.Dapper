using Dapper.Extensions.DataAccess.Sqlite.Tests.Models;

namespace Dapper.Extensions.DataAccess.Sqlite.Tests;

public class MapperDefinitionProviderTest
{
    [Fact]
    public Task TestThrowArgumentExceptionAsync()
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices((context, services) =>
        {
            services.AddSqliteDataAccess(configurationPath: "MapperDefinitionProviderTest.dapper.xml");
            services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
        });
        var app = host.Build();

        Assert.Throws<ArgumentException>(() => app.Services.GetRequiredService<IMapperDefinitionProvider<Album, int>>());
        return Task.CompletedTask;
    }
}