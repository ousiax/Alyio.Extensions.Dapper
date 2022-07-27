using Dapper;

namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents a general implementation of data store service.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity mapped to relation database.</typeparam>
    /// <typeparam name="TId">The ID (i.e. primary key) type of the entity.</typeparam>
    public class StoreService<TEntity, TId> : IStoreService<TEntity, TId> where TEntity : class, new()
    {
        /// <summary>
        /// Gets a <see cref="IConnectionFactory"/>.
        /// </summary>
        protected IConnectionFactory ConnectionFactory { get; }

        /// <summary>
        /// Gets a <see cref="IMapperDefinitionProvider{TEntity, TId}"/>.
        /// </summary>
        protected IMapperDefinitionProvider<TEntity, TId> Mapper { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="StoreService{TEntity, TId}"/>.
        /// </summary>
        /// <param name="connectionFactory">A <see cref="IConnectionFactory"/>.</param>
        /// <param name="mapperProvider">A <see cref="IMapperDefinitionProvider{TEntity, TId}"/>.</param>
        public StoreService(IConnectionFactory connectionFactory, IMapperDefinitionProvider<TEntity, TId> mapperProvider)
        {
            ConnectionFactory = connectionFactory;
            Mapper = mapperProvider;
        }

        /// <inheritdoc/>
        public async Task<T?> QuerySingleOrDefaultByIdAsync<T>(string sqlDefId, TId id, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(def.OpenMode).ConfigureAwait(false);
            var param = new DynamicParameters();
            param.Add(def.IdName, id);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: param,
                cancellationToken: cancellationToken);
            return await conn.QuerySingleOrDefaultAsync<T>(cmdDef).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sqlDefId, object? param = null, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(def.OpenMode).ConfigureAwait(false);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: param,
                cancellationToken: cancellationToken);
            return await conn.QueryAsync<T>(cmdDef).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<(int totalCount, IEnumerable<T> results)> PageQueryAsync<T>(string sqlDefId, int pageNumber, int pageSize, object? param, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(string sqlDefId, object? param, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindInsert(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(OpenMode.ReadWrite).ConfigureAwait(false);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: param,
                cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteByIdAsync(string sqlDefId, TId id, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindDelete(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(OpenMode.ReadWrite).ConfigureAwait(false);
            var param = new DynamicParameters();
            param.Add(def.IdName, id);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: param,
                cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string sqlDefId, object? param, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindDelete(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(OpenMode.ReadWrite).ConfigureAwait(false);

            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: param,
                cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<int> UpdateAsync(string sqlDefId, object? param, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindUpdate(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(OpenMode.ReadWrite).ConfigureAwait(false);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: param,
                cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }
    }
}
