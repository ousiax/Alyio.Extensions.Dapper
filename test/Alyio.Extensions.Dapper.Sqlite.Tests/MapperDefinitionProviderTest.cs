using Alyio.Extensions.Dapper.Sqlite.Tests.Models;

namespace Alyio.Extensions.Dapper.Sqlite.Tests
{
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

            var exception = Assert.Throws<ArgumentException>(() => app.Services.GetRequiredService<IMapperDefinitionProvider<Album, int>>());
            Assert.Contains($"The mapper of type `{typeof(Album)}` was not defined", exception.Message);

            return Task.CompletedTask;
        }
    }
}