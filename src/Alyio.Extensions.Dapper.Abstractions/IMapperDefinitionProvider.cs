﻿namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents the definition of mapped SQL statements.
    /// </summary>
    public interface IMapperDefinitionProvider<TEntity, TId> where TEntity : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        bool TryFindSelect(string id, out SelectDefinition definition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        bool TryFindInsert(string id, out InsertDefinition definition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        bool TryFindDelete(string id, out DeleteDefinition definition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        bool TryFindUpdate(string id, out UpdateDefinition definition);
    }
}