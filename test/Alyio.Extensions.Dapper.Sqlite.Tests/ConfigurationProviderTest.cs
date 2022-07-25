namespace Alyio.Extensions.Dapper.Sqlite.Tests
{
    public class ConfigurationProviderTest
    {
        [Fact]
        public Task TestThrowArgumentExceptionAsync()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                var host = Host.CreateDefaultBuilder();
                host.ConfigureServices((context, services) =>
                {
                    services.AddSqliteDataAccess(configurationPath: "ConfigurationProviderTest.dapper.xml");
                    services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
                });
                host.Build();
            });

            Assert.Contains("Couldn't get manifest resource of type", exception.Message);

            return Task.CompletedTask;
        }
    }
}