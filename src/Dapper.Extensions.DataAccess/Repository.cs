﻿namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        protected IConnectionFactory ConnectionFactory { get; }

        /// <summary>
        /// 
        /// </summary>
        protected IMapperDefinitionProvider<TEntity, TId> Mapper { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionFactory"></param>
        /// <param name="mapperProvider"></param>
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
            using var conn = await ConnectionFactory.OpenAsync().ConfigureAwait(false);
            var parameters = new DynamicParameters();
            parameters.Add(def.IdName, id);
            var cmdDef = new CommandDefinition(
                commandText: def.CommandText,
                commandTimeout: def.CommandTimeout,
                commandType: def.CommandType,
                parameters: parameters,
                cancellationToken: cancellationToken);
            return await conn.QuerySingleAsync<TEntity>(cmdDef).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> SelectAllAsync(CancellationToken cancellationToken = default)
        {
            if (!Mapper.TryFindSelect(nameof(SelectAllAsync), out var def))
            {
                throw new ArgumentException($"The given id '{nameof(SelectAllAsync)}' was not present in the mapper.");
            }
            using var conn = await ConnectionFactory.OpenAsync().ConfigureAwait(false);
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
            using var conn = await ConnectionFactory.OpenAsync().ConfigureAwait(false);
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
            using var conn = await ConnectionFactory.OpenAsync().ConfigureAwait(false);
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
            using var conn = await ConnectionFactory.OpenAsync().ConfigureAwait(false);
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
