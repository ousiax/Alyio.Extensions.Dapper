namespace Dapper.Extensions.DataAccess
{
    internal sealed class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity<TId>, new()
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IMapperProvider<TEntity, TId> _mapperProvider;

        public Repository(IDbConnectionFactory connectionFactory, IMapperProvider<TEntity, TId> mapperProvider)
        {
            _connectionFactory = connectionFactory;
            _mapperProvider = mapperProvider;
        }

        public async Task<TEntity> SelectByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var def = _mapperProvider.SelectDefinitions[nameof(SelectByIdAsync)];
            var queryParams = new DynamicParameters();
            queryParams.Add(def.IdName, id);
            var cmdDef = new CommandDefinition(commandText: def.Sql, parameters: queryParams, cancellationToken: cancellationToken);
            return await conn.QuerySingleAsync<TEntity>(cmdDef).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> SelectAllAsync(CancellationToken cancellationToken = default)
        {
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var def = _mapperProvider.SelectDefinitions[nameof(SelectAllAsync)];
            var cmdDef = new CommandDefinition(commandText: def.Sql, cancellationToken: cancellationToken);
            return await conn.QueryAsync<TEntity>(cmdDef).ConfigureAwait(false);
        }

        public async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var def = _mapperProvider.InsertDefinitions[nameof(InsertAsync)];
            var cmdDef = new CommandDefinition(commandText: def.Sql, parameters: entity, cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }

        public async Task<int> DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var def = _mapperProvider.DeleteDefinitions[nameof(DeleteAsync)];
            var queryParams = new DynamicParameters();
            queryParams.Add(def.IdName, id);
            var cmdDef = new CommandDefinition(commandText: def.Sql, parameters: queryParams, cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);

        }

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using var conn = await _connectionFactory.OpenAsync().ConfigureAwait(false);
            var def = _mapperProvider.UpdateDefinitions[nameof(UpdateAsync)];
            var cmdDef = new CommandDefinition(commandText: def.Sql, parameters: entity, cancellationToken: cancellationToken);
            return await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
        }
    }
}
