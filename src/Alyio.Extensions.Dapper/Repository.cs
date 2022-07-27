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
        /// Gets a <see cref="IStoreService{TEntity, TId}"/>.
        /// </summary>
        protected IStoreService<TEntity, TId> Store { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Repository{TEntity, TId}"/>.
        /// </summary>
        /// <param name="storeService">A <see cref="IMapperDefinitionProvider{TEntity, TId}"/>.</param>
        public Repository(IStoreService<TEntity, TId> storeService)
        {
            Store = storeService;
        }

        /// <inheritdoc/>
        public Task<TEntity?> SelectByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return Store.QuerySingleOrDefaultByIdAsync<TEntity>(nameof(SelectByIdAsync), id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> SelectAllAsync(CancellationToken cancellationToken = default)
        {
            return Store.QueryAsync<TEntity>(nameof(SelectAllAsync), cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Store.InsertAsync(nameof(InsertAsync), entity, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            return Store.DeleteByIdAsync(nameof(DeleteAsync), id, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Store.UpdateAsync(nameof(UpdateAsync), entity, cancellationToken: cancellationToken);
        }
    }
}
