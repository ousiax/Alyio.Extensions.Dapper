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

            if (string.IsNullOrEmpty(def.IdName))
            {
                throw new ArgumentException($"The {nameof(SelectDefinition.IdName)} is null or empty.");
            }

            var param = new DynamicParameters();
            param.Add(def.IdName, id);
            var results = await QueryAsync<T>(def, param, cancellationToken);
            return results.FirstOrDefault();
        }

        /// <inheritdoc/>
        public Task<IEnumerable<T>> QueryAsync<T>(string sqlDefId, object? param = null, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }

            return QueryAsync<T>(def, param, cancellationToken);
        }

        /// <summary>
        /// Execute a query and return a sequence of data of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="def">A <see cref="SelectDefinition"/>.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>
        /// A sequence of data of <typeparamref name="T"/>; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        protected async Task<IEnumerable<T>> QueryAsync<T>(SelectDefinition def, object? param = null, CancellationToken cancellationToken = default)
        {
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
        public async Task<int> InsertAsync(string sqlDefId, object? param = null, CancellationToken cancellationToken = default)
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
        public Task<int> DeleteByIdAsync(string sqlDefId, TId id, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindDelete(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }

            if (string.IsNullOrEmpty(def.IdName))
            {
                throw new ArgumentException($"The {nameof(SelectDefinition.IdName)} is null or empty.");
            }

            var param = new DynamicParameters();
            param.Add(def.IdName, id);
            return DeleteAsync(def, param, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(string sqlDefId, object? param = null, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindDelete(sqlDefId, out var def))
            {
                throw new ArgumentException($"The given id '{sqlDefId}' was not present in the mapper.");
            }

            return DeleteAsync(def, param, cancellationToken);
        }

        private async Task<int> DeleteAsync(DeleteDefinition def, object? param = null, CancellationToken cancellationToken = default)
        {
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
        public async Task<int> UpdateAsync(string sqlDefId, object? param = null, CancellationToken cancellationToken = default)
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
