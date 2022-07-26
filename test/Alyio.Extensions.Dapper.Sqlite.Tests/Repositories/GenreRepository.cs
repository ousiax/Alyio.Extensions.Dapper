using Alyio.Extensions.Dapper.Sqlite.Tests.Models;

using Dapper;

namespace Alyio.Extensions.Dapper.Sqlite.Tests.Repositories
{
    // CREATE TABLE [Genre]
    // (
    //     [GenreId] INTEGER  NOT NULL,
    //     [Name] NVARCHAR(120),
    //     CONSTRAINT [PK_Genre] PRIMARY KEY  ([GenreId])
    // );
    ///
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
                throw new ArgumentException($"The given id '{nameof(SelectByIdAsync)}' was not present in the mapper.");
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

        public async Task<(int pageCount, IEnumerable<Genre>)> PageSelectAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(nameof(SelectAllAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(SelectAllAsync)}' was not present in the mapper.");
            }

            def = def.AsPageable(pageNumber, pageSize);
            using var conn = await ConnectionFactory.OpenAsync(def.OpenMode).ConfigureAwait(false);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                cancellationToken: cancellationToken);
            using var multi = await conn.QueryMultipleAsync(cmdDef).ConfigureAwait(false);
            var count = await multi.ReadSingleAsync<int>();
            var results = await multi.ReadAsync<Genre>();
            var pageCount = (int)Math.Ceiling((double)count / pageSize);
            return (pageCount, results);
        }
    }
}