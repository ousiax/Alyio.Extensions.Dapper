using System.Data;

namespace Alyio.Extensions.Dapper
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
