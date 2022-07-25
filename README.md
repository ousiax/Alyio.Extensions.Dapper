# Alyio.Extensions.Dapper

![Build Status](https://github.com/qqbuby/Alyio.Extensions.Dapper/actions/workflows/ci.yml/badge.svg?branch=main)

_Alyio.Extensions.Dapper_ provides extensions for [Dapper](https://github.com/DapperLib/Dapper) that implements a [repository pattern] (https://martinfowler.com/eaaCatalog/repository.html).

## Usage:

```sh
dotnet add package Alyio.Extensions.Dapper.MySql --version 1.0.0
```

1. Define the `dapper.xml` and sql statement mapper files.

    Here is a sample of `dapper.xml`:

    ```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <configuration>
      <mappers>
        <mapper type="ChinookApp.Models.Genre, ChinookApp"></mapper>
        <mapper type="ChinookApp.Models.Artist, ChinookApp"></mapper>
        <mapper type="ChinookApp.Models.Customer, ChinookApp"></mapper>
      </mappers>
    </configuration>
    ```

    And here is the embeded resource named `Genre.xml` for MySQL (e.g. _mapper.xml_ as above) where is also located as the same directory of type `Genre`:

    ```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <mapper>
      <select id="SelectByIdAsync" idName="GenreId" commandType="Text">SELECT GenreId, Name FROM Genre WHERE GenreId = @GenreId</select>
      <select id="SelectByNameAsync">SELECT GenreId, Name FROM Genre WHERE Name = @Name</select>
      <select id="SelectAllAsync">SELECT GenreId, Name FROM Genre</select>
      <select id="SelectPageAsync">SELECT COUNT(*) FROM Genre;SELECT GenreId, Name FROM Genre LIMIT @LIMIT OFFSET @OFFSET;</select>
      <insert id="InsertAsync">INSERT INTO Genre(GenreId, Name) VALUES (@GenreId, @Name)</insert>
      <delete id="DeleteAsync" idName="GenreId">DELETE FROM Genre WHERE GenreId = @GenreId</delete>
      <update id="UpdateAsync" idName="GenreId">UPDATE Genre SET NAME = @Name WHERE GenreId = @GenreId</update>
    </mapper>
    ```
    
    The `SelectPageAsync` is a paginator SQL statements mapper and the follow custom repository `GenreRepository.SelectPageAsync` implements the paging query.

2. (Optional), extend the generic repository `Repository<,>` to provide custom methods, for example:

    ```cs
    public class GenreRepository : Repository<Genre, int>, IGenreRepository
    {
        public GenreRepository(
            IConnectionFactory connectionFactory,
            IMapperDefinitionProvider<Genre, int> mapperProvider) : base(connectionFactory, mapperProvider)
        {
        }

        public async Task<Genre> SelectByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(nameof(SelectByNameAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(SelectByNameAsync)}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync().ConfigureAwait(false);
            var parameters = new DynamicParameters();
            parameters.Add(nameof(Genre.Name), name);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: parameters,
                cancellationToken: cancellationToken);
            return await conn.QuerySingleOrDefaultAsync<Genre>(cmdDef).ConfigureAwait(false);
        }

        public async Task<(int pageCount, IEnumerable<Genre> resultSet)> SelectPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            if (pageNumber < 0)
            {
                throw new ArgumentException($"{nameof(pageNumber)} must be greater than or equal to zero.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException($"{nameof(pageSize)} must be greater than zero.");
            }

            if (!Mapper.TryFindSelect(nameof(SelectPageAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(SelectPageAsync)}' was not present in the mapper.");
            }

            using var conn = await ConnectionFactory.OpenAsync().ConfigureAwait(false);
            var parameters = new DynamicParameters();
            parameters.Add("LIMIT", pageSize);
            parameters.Add("OFFSET", pageSize * pageNumber);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: parameters,
                cancellationToken: cancellationToken);
            using var multi = await conn.QueryMultipleAsync(cmdDef).ConfigureAwait(false);
            var count = await multi.ReadSingleAsync<int>();
            var results = await multi.ReadAsync<Genre>();
            return (count / pageSize, results);
        }
    }
    ```

3. Set up the generic repository services in IoC container.

    ```cs
    services.AddMySqlDataAccess();
    services.Configure<MySqlConnectionOptions>(context.Configuration.GetSection(nameof(MySqlConnectionOptions)));
    services.AddScoped<IGenreRepository, GenreRepository>();
    ```

4. Get and invoke the specified repository service:

    ```cs
    var genreRepository = app.Services.GetRequiredService<IGenreRepository>();
    var genres2 = await genreRepository.SelectAllAsync();
    foreach (var artist in genres.Take(10))
    {
        Console.WriteLine(artist.Name);
    }
    ```

## There is also an example at _exmaple/ChinookApp_:

```cs
// using statements ...

var host = Host.CreateDefaultBuilder();

// Configure MySqlConnectionOption from user secrets.
host.ConfigureAppConfiguration(builder => builder.AddUserSecrets(typeof(Program).Assembly, false));

host.ConfigureServices((context, services) =>
{
    services.AddMySqlDataAccess();
    services.Configure<MySqlConnectionOptions>(context.Configuration.GetSection(nameof(MySqlConnectionOptions)));
    services.AddScoped<IGenreRepository, GenreRepository>();
});
var app = host.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
MySqlConnectorLogManager.Provider = new MicrosoftExtensionsLoggingLoggerProvider(loggerFactory);

var genericRepository = app.Services.GetRequiredService<IRepository<Genre, int>>();
var genres = await genericRepository.SelectAllAsync();
foreach (var artist in genres.Take(10))
{
    Console.WriteLine(artist.Name);
}

Console.WriteLine("--------------");
var genreRepository = app.Services.GetRequiredService<IGenreRepository>();
var genres2 = await genreRepository.SelectAllAsync();
foreach (var artist in genres.Take(10))
{
    Console.WriteLine(artist.Name);
}

Console.WriteLine("--------------");
var genre3 = await genreRepository.SelectByNameAsync(genres2.First()!.Name!);
Console.WriteLine(genre3.Name);

Console.WriteLine("--------------");
var (pageCount, resultSet) = await genreRepository.SelectPageAsync(1, 5);
Console.WriteLine(pageCount);
foreach (var g in resultSet)
{
    Console.WriteLine(g.Name);
}
```

```console
$ dotnet run
info: MySqlConnector.ConnectionPool[0]
      Pool1 creating new connection pool for ConnectionString: Server=local.io;Port=53306;User ID=root;Database=Chinook;Pooling=True;Minimum Pool Size=64;Maximum Pool Size=512
Rock
Jazz
Metal
Alternative & Punk
Rock And Roll
Blues
Latin
Reggae
Pop
Soundtrack
--------------
Rock
Jazz
Metal
Alternative & Punk
Rock And Roll
Blues
Latin
Reggae
Pop
Soundtrack
--------------
Rock
--------------
5
Blues
Latin
Reggae
Pop
Soundtrack
info: MySqlConnector.ConnectionPool[0]
      Pool1 clearing connection pool
```
