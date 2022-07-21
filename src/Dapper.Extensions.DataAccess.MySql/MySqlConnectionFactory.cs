using System.Data;

using Microsoft.Extensions.Options;

using MySqlConnector;

namespace Dapper.Extensions.DataAccess
{
    internal sealed class MySqlConnectionFactory : IConnectionFactory
    {
        private readonly IOptions<MySqlConnectionOptions> _options;

        public MySqlConnectionFactory(IOptions<MySqlConnectionOptions> options)
        {
            _options = options;
        }

        public async Task<IDbConnection> OpenAsync()
        {
            var conn = new MySqlConnection(_options.Value.Master);
            await conn.OpenAsync().ConfigureAwait(false);
            return conn;
        }

        public async Task<IDbConnection> OpenReadOnlyAsync()
        {
            var conn = new MySqlConnection(_options.Value.Slave?[0]);
            await conn.OpenAsync().ConfigureAwait(false);
            return conn;
        }
    }
}
