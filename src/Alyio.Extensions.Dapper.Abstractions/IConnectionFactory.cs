using System.Data;

namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents a type used to create instance of <see cref="IDbConnection"/>.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Create and open an instance of <see cref="IDbConnection"/>.
        /// </summary>
        /// <param name="mode"><see cref="OpenMode"/>.</param>
        /// <returns><see cref="IDbConnection"/>.</returns>
        Task<IDbConnection> OpenAsync(OpenMode mode = OpenMode.ReadWrite);
    }
}
