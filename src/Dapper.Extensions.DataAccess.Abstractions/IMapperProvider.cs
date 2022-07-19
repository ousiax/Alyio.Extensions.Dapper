namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// Represents the definition of mapped SQL statements.
    /// </summary>
    public interface IMapperProvider<TEntity, TId> where TEntity : IEntity<TId>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, SelectDefinition> SelectDefinitions { get; }

        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, InsertDefinition> InsertDefinitions { get; }

        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, DeleteDefinition> DeleteDefinitions { get; }

        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, UpdateDefinition> UpdateDefinitions { get; }
    }
}
