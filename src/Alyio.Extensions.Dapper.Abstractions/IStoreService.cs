namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents a general data store service.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity mapped to relation database.</typeparam>
    /// <typeparam name="TId">The ID (i.e. primary key) type of the entity.</typeparam>
    public interface IStoreService<TEntity, TId> where TEntity : class, new()
    {
        /// <summary>
        /// Execute a single-row query by <paramref name="id"/> and return an instance of type <typeparamref name="T"/>, or null if the query returns empty.
        /// </summary>
        /// <param name="sqlDefId">The unique identifier that can be used to reference an instance of <see cref="SelectDefinition"/>.</param>
        /// <param name="id">The id (i.e. primary key) of the <typeparamref name="TEntity"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>
        /// An instance of type <typeparamref name="T"/>; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive), or default(T) if the query returns empty.
        /// </returns>
        /// <exception cref="ArgumentException">The <see cref="SelectDefinition.IdName"/> is null or empty.</exception>
        Task<T?> QuerySingleOrDefaultByIdAsync<T>(string sqlDefId, TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute a query and return a sequence of data of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="sqlDefId">The unique identifier that can be used to reference an instance of <see cref="SelectDefinition"/>.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>
        /// A sequence of data of <typeparamref name="T"/>; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sqlDefId, object? param = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute a query and return binary tuple of total count and a sequence of data of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="sqlDefId">The unique identifier that can be used to reference an instance of <see cref="SelectDefinition"/>.</param>
        /// <param name="pageNumber">The page number based-zero. If the page number is less than zero, it will be reset to zero.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A binary tuple of total cound and a sequence of data of <typeparamref name="T"/>.</returns>
        /// <exception cref="ArgumentException">The page size must be greater than zero.</exception>
        Task<(int totalCount, IEnumerable<T> results)> PageQueryAsync<T>(string sqlDefId, int pageNumber, int pageSize, object? param = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts an instance of type <typeparamref name="TEntity"/> into relation database.
        /// </summary>
        /// <param name="sqlDefId">The unique identifier that can be used to reference an instance of <see cref="InsertDefinition"/>.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> InsertAsync(string sqlDefId, object? param = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an instance of type <typeparamref name="TEntity"/> from relation database by primary key <paramref name="id"/>.
        /// </summary>
        /// <param name="sqlDefId">The unique identifier that can be used to reference an instance of <see cref="DeleteDefinition"/>.</param>
        /// <param name="id">The id (i.e. primary key) of the <typeparamref name="TEntity"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="ArgumentException">The <see cref="SelectDefinition.IdName"/> is null or empty.</exception>
        Task<int> DeleteByIdAsync(string sqlDefId, TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an instance of type <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="sqlDefId">The unique identifier that can be used to reference an instance of <see cref="DeleteDefinition"/>.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> DeleteAsync(string sqlDefId, object? param = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an instance of type <typeparamref name="TEntity"/> in the relation database.
        /// </summary>
        /// <param name="sqlDefId">The unique identifier that can be used to reference an instance of <see cref="UpdateDefinition"/>.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> UpdateAsync(string sqlDefId, object? param = null, CancellationToken cancellationToken = default);
    }
}
