using Dapper;

namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents a MySql general data store service.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity mapped to relation database.</typeparam>
    /// <typeparam name="TId">The ID (i.e. primary key) type of the entity.</typeparam>
    public class MySqlStoreService<TEntity, TId> : StoreService<TEntity, TId> where TEntity : class, new()
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MySqlStoreService{TEntity, TId}"/>.
        /// </summary>
        /// <param name="connectionFactory">A <see cref="IConnectionFactory"/>.</param>
        /// <param name="mapperProvider">A <see cref="IMapperDefinitionProvider{TEntity, TId}"/>.</param>
        public MySqlStoreService(IConnectionFactory connectionFactory, IMapperDefinitionProvider<TEntity, TId> mapperProvider) : base(connectionFactory, mapperProvider)
        {
        }

        /// <inheritdoc/>
        public override async Task<(int totalCount, IEnumerable<T> results)> PageQueryAsync<T>(string sqlDefId, int pageNumber, int pageSize, object? param, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }

            def = def.AsPageable(pageNumber, pageSize);

            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: param,
                cancellationToken: cancellationToken);
            using var conn = await ConnectionFactory.OpenAsync().ConfigureAwait(false);
            using var multi = await conn.QueryMultipleAsync(cmdDef).ConfigureAwait(false);
            var totalCount = await multi.ReadSingleAsync<int>();
            var results = await multi.ReadAsync<T>();
            return (totalCount, results);
        }
    }
}
