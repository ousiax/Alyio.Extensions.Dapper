namespace Dapper.Extensions.DataAccess
{
    internal sealed class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, new()
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IMapperDefinitionProvider<TEntity, TId> _mapperProvider;

        public Repository(IConnectionFactory connectionFactory, IMapperDefinitionProvider<TEntity, TId> mapperProvider)
        {
            _connectionFactory = connectionFactory;
            _mapperProvider = mapperProvider;
        }

        public async Task<TEntity> SelectByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            if (!_mapperProvider.TryFindSelect(nameof(SelectByIdAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(SelectByIdAsync)}' was not present in the mapper.");
            }
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var queryParams = new DynamicParameters();
            queryParams.Add(def.IdName, id);
            var cmdDef = new CommandDefinition(commandText: def.Sql, parameters: queryParams, cancellationToken: cancellationToken);
            return await conn.QuerySingleAsync<TEntity>(cmdDef).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> SelectAllAsync(CancellationToken cancellationToken = default)
        {
            if (!_mapperProvider.TryFindSelect(nameof(SelectAllAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(SelectAllAsync)}' was not present in the mapper.");
            }
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var cmdDef = new CommandDefinition(commandText: def.Sql, cancellationToken: cancellationToken);
            return await conn.QueryAsync<TEntity>(cmdDef).ConfigureAwait(false);
        }

        public async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!_mapperProvider.TryFindInsert(nameof(InsertAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(InsertAsync)}' was not present in the mapper.");
            }
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var cmdDef = new CommandDefinition(commandText: def.Sql, parameters: entity, cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }

        public async Task<int> DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            if (!_mapperProvider.TryFindDelete(nameof(DeleteAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(DeleteAsync)}' was not present in the mapper.");
            }
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var queryParams = new DynamicParameters();
            queryParams.Add(def.IdName, id);
            var cmdDef = new CommandDefinition(commandText: def.Sql, parameters: queryParams, cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);

        }

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!_mapperProvider.TryFindUpdate(nameof(UpdateAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(UpdateAsync)}' was not present in the mapper.");
            }
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var cmdDef = new CommandDefinition(commandText: def.Sql, parameters: entity, cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }
    }
}
