using Dapper;

using System.Data;
using System.Globalization;

namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents a SqLite general data store service.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity mapped to relation database.</typeparam>
    /// <typeparam name="TId">The ID (i.e. primary key) type of the entity.</typeparam>
    public class SqliteStoreService<TEntity, TId> : StoreService<TEntity, TId> where TEntity : class, new()
    {
        private static readonly string PageableTextForamt = "SELECT COUNT(*) FROM ({0}) original_query; {0} LIMIT {1} OFFSET {2};";

        /// <summary>
        /// Initializes a new instance of <see cref="SqliteStoreService{TEntity, TId}"/>.
        /// </summary>
        /// <param name="connectionFactory">A <see cref="IConnectionFactory"/>.</param>
        /// <param name="mapperProvider">A <see cref="IMapperDefinitionProvider{TEntity, TId}"/>.</param>
        public SqliteStoreService(IConnectionFactory connectionFactory, IMapperDefinitionProvider<TEntity, TId> mapperProvider) : base(connectionFactory, mapperProvider)
        {
        }

        /// <inheritdoc/>
        public override async Task<(int totalCount, IEnumerable<T> results)> PageQueryAsync<T>(string sqlDefId, int pageNumber, int pageSize, object? param, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }

            def = AsPageable(def, pageNumber, pageSize);

            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: param,
                cancellationToken: cancellationToken);
            using var conn = await ConnectionFactory.OpenAsync().ConfigureAwait(false);
            using var multi = await conn.QueryMultipleAsync(cmdDef).ConfigureAwait(false);
            var totalCount = await multi.ReadSingleAsync<int>().ConfigureAwait(false);
            var results = await multi.ReadAsync<T>().ConfigureAwait(false);
            return (totalCount, results);
        }

        /// <summary>
        /// Converts a <see cref="SelectDefinition"/> to a pageable instance.
        /// </summary>
        /// <param name="def">The <see cref="SelectDefinition"/>.</param>
        /// <param name="pageNumber">The page number based-zero. If the page number is less than zero, it will be reset to zero.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A pageable select sql definition to this instance.</returns>
        /// <exception cref="InvalidCastException">The command type of <paramref name="def"/> must be <see cref="CommandType.Text"/>.</exception>
        /// <exception cref="ArgumentException">The page size must be greater than zero.</exception>
        private static SelectDefinition AsPageable(SelectDefinition def, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 0;
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException($"The {nameof(pageSize)} must be greater than zero.");
            }

            if (def.CommandType != CommandType.Text)
            {
                throw new InvalidCastException($"The command type of {nameof(def)} must be CommandType.Text.");
            }

            var pageable = new SelectDefinition
            {
                Id = def.Id,
                CommandText = string.Format(CultureInfo.InvariantCulture, PageableTextForamt, def.CommandText, pageSize, pageNumber * pageSize),
                CommandTimeout = def.CommandTimeout,
                CommandType = def.CommandType,
                IdName = def.IdName,
                OpenMode = def.OpenMode,
            };

            return pageable;
        }
    }
}
