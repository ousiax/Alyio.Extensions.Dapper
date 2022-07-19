using Dapper.Extensions.DataAccess.Sqlite.Tests.Models;

namespace Dapper.Extensions.DataAccess.Sqlite.Tests;

public class RepositoryTest
{
    private IServiceProvider Services { get; }

    public RepositoryTest()
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices((context, services) =>
        {
            services.AddSqliteDataAccess();
            services.Configure<SqliteConnectionOptions>(context.Configuration.GetSection(nameof(SqliteConnectionOptions)));
        });
        var app = host.Build();
        Services = app.Services;
        // var connectionFactory = Services.GetRequiredService<IDbConnectionFactory>();
        // using var conn = connectionFactory.OpenAsync().Result;
        // var chinookSql = File.ReadAllText("Chinook_Sqlite.sql");
        // conn.ExecuteAsync(chinookSql);
    }

    [Fact]
    public async Task TestSelectAsync()
    {
        var genreRepo = Services.GetRequiredService<IRepository<Genre, int>>();
        await genreRepo.SelectByIdAsync(1);
        await genreRepo.SelectAllAsync();
    }
}