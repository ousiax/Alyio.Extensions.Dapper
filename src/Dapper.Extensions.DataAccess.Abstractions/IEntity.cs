namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        /// 
        /// </summary>
        TId Id { get; set; }
    }
}
