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
        /// <param name="mode"></param>
        /// <returns></returns>
        Task<IDbConnection> OpenAsync(OpenMode mode = OpenMode.ReadWrite);
    }
}
