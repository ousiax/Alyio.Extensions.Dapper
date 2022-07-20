using System.Data;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IDbConnection> OpenAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IDbConnection> OpenReadOnlyAsync();
    }
}
