namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents the definition of mapped SQL statements.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity mapped to relation database.</typeparam>
    /// <typeparam name="TId">The ID (i.e. primary key) type of the entity.</typeparam>
    public interface IMapperDefinitionProvider<TEntity, TId> where TEntity : class, new()
    {
        /// <summary>
        /// Get a <see cref="SelectDefinition"/> by <paramref name="id"/>. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="id"><see cref="SqlDefinition.Id"/></param>
        /// <param name="definition">A <see cref="SelectDefinition"/>.</param>
        /// <returns>true if the operation was successful; otherwise, false.</returns>
        bool TryFindSelect(string id, out SelectDefinition definition);

        /// <summary>
        /// Get a <see cref="InsertDefinition"/> by <paramref name="id"/>. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="id"><see cref="SqlDefinition.Id"/></param>
        /// <param name="definition">A <see cref="InsertDefinition"/>.</param>
        /// <returns>true if the operation was successful; otherwise, false.</returns>
        bool TryFindInsert(string id, out InsertDefinition definition);

        /// <summary>
        /// Get a <see cref="DeleteDefinition"/> by <paramref name="id"/>. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="id"><see cref="SqlDefinition.Id"/></param>
        /// <param name="definition">A <see cref="DeleteDefinition"/>.</param>
        /// <returns>true if the operation was successful; otherwise, false.</returns>
        bool TryFindDelete(string id, out DeleteDefinition definition);

        /// <summary>
        /// Get a <see cref="UpdateDefinition"/> by <paramref name="id"/>. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="id"><see cref="SqlDefinition.Id"/></param>
        /// <param name="definition">A <see cref="UpdateDefinition"/>.</param>
        /// <returns>true if the operation was successful; otherwise, false.</returns>
        bool TryFindUpdate(string id, out UpdateDefinition definition);
    }
}
