using Alyio.Extensions.Dapper;

using ChinookApp.Models;

using Dapper;

namespace ChinookApp.Repositories
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
        /// <summary>
        /// Gets a <see cref="IConnectionFactory"/>.
        /// </summary>
        private IConnectionFactory ConnectionFactory { get; }

        /// <summary>
        /// Gets a <see cref="IMapperDefinitionProvider{TEntity, TId}"/>.
        /// </summary>
        private IMapperDefinitionProvider<Genre, int> Mapper { get; }

        public GenreRepository(
            IConnectionFactory connectionFactory,
            IMapperDefinitionProvider<Genre, int> mapperProvider,
            IStoreService<Genre, int> storeService) : base(storeService)
        {
            ConnectionFactory = connectionFactory;
            Mapper = mapperProvider;
        }

        public async Task<Genre?> SelectByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var parameters = new DynamicParameters();
            parameters.Add(nameof(Genre.Name), name);
            var genres = await Store.QueryAsync<Genre>(nameof(SelectByNameAsync), parameters, cancellationToken).ConfigureAwait(false);
            return genres.FirstOrDefault();
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
            var pageCount = (int)Math.Ceiling((double)count / pageSize);
            return (pageCount, results);
        }
    }
}