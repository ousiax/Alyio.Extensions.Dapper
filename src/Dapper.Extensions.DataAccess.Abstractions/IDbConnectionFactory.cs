using System.Data;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbConnectionFactory
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
