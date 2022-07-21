using System.Data;

using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace Dapper.Extensions.DataAccess
{
    internal sealed class SqliteConnectionFactory : IConnectionFactory
    {
        private readonly IOptions<SqliteConnectionOptions> _options;

        public SqliteConnectionFactory(IOptions<SqliteConnectionOptions> options)
        {
            _options = options;
        }

        public async Task<IDbConnection> OpenAsync()
        {
            var conn = new SqliteConnection(_options.Value.Master);
            await conn.OpenAsync().ConfigureAwait(false);
            return conn;
        }

        public async Task<IDbConnection> OpenReadOnlyAsync()
        {
            var conn = new SqliteConnection(_options.Value.Slaves?[0]);
            await conn.OpenAsync().ConfigureAwait(false);
            return conn;
        }
    }
}
