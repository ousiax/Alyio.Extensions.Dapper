namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// Represents the definition of mapped SQL statements.
    /// </summary>
    public interface IMapperProvider<TEntity, TId> where TEntity : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        bool TryFindSelectDefinition(string id, out SelectDefinition definition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        bool TryFindInsertDefinition(string id, out InsertDefinition definition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        bool TryFindDeleteDefinition(string id, out DeleteDefinition definition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        bool TryFindUpdateDefinition(string id, out UpdateDefinition definition);
    }
}
