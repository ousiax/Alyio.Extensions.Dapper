using Dapper;

namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents a generic repository implementation.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity mapped to relation database.</typeparam>
    /// <typeparam name="TId">The ID (i.e. primary key) type of the entity.</typeparam>
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, new()
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
        /// Initializes a new instance of <see cref="Repository{TEntity, TId}"/>.
        /// </summary>
        /// <param name="connectionFactory">A <see cref="IConnectionFactory"/>.</param>
        /// <param name="mapperProvider">A <see cref="IMapperDefinitionProvider{TEntity, TId}"/>.</param>
        public Repository(IConnectionFactory connectionFactory, IMapperDefinitionProvider<TEntity, TId> mapperProvider)
        {
            ConnectionFactory = connectionFactory;
            Mapper = mapperProvider;
        }

        /// <inheritdoc/>
        public async Task<TEntity> SelectByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(nameof(SelectByIdAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(SelectByIdAsync)}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(def.OpenMode).ConfigureAwait(false);
            var parameters = new DynamicParameters();
            parameters.Add(def.IdName, id);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: parameters,
                cancellationToken: cancellationToken);
            return await conn.QuerySingleOrDefaultAsync<TEntity>(cmdDef).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> SelectAllAsync(CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(nameof(SelectAllAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(SelectAllAsync)}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(def.OpenMode).ConfigureAwait(false);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                cancellationToken: cancellationToken);
            return await conn.QueryAsync<TEntity>(cmdDef).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindInsert(nameof(InsertAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(InsertAsync)}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(OpenMode.ReadWrite).ConfigureAwait(false);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: entity,
                cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindDelete(nameof(DeleteAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(DeleteAsync)}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(OpenMode.ReadWrite).ConfigureAwait(false);
            var parameters = new DynamicParameters();
            parameters.Add(def.IdName, id);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: parameters,
                cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);

        }

        /// <inheritdoc/>
        public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindUpdate(nameof(UpdateAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(UpdateAsync)}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync(OpenMode.ReadWrite).ConfigureAwait(false);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: entity,
                cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }
    }
}
