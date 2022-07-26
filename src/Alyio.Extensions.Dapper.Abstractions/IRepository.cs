namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents a generic repository interface.
    /// <seealso href="https://martinfowler.com/eaaCatalog/repository.html"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity mapped to relation database.</typeparam>
    /// <typeparam name="TId">The ID (i.e. primary key) type of the entity.</typeparam>
    public interface IRepository<TEntity, TId> where TEntity : class, new()
    {
        /// <summary>
        /// Execute a single-row query by <paramref name="id"/> and return an instance of type <typeparamref name="TEntity"/>, or null if the query returns empty.
        /// </summary>
        /// <param name="id">The id (i.e. primary key) of the <typeparamref name="TEntity"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>An instance of type <typeparamref name="TEntity"/>, or null if the query returns empty.</returns>
        Task<TEntity?> SelectByIdAsync(TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute a query and return a sequence of data of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A sequence of data of <typeparamref name="TEntity"/>.</returns>
        Task<IEnumerable<TEntity>> SelectAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts an instance of type <typeparamref name="TEntity"/> into relation database.
        /// </summary>
        /// <param name="entity">A entity instance of type <typeparamref name="TEntity"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an instance of type <typeparamref name="TEntity"/> from relation database by primary key <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id (i.e. primary key) of the <typeparamref name="TEntity"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> DeleteAsync(TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an instance of type <typeparamref name="TEntity"/> in the relation database.
        /// </summary>
        /// <param name="entity">A entity instance of type <typeparamref name="TEntity"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
