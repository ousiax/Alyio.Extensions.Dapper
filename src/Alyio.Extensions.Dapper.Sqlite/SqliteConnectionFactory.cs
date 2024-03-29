﻿using System.Data;

using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace Alyio.Extensions.Dapper
{
    internal sealed class SqliteConnectionFactory : IConnectionFactory
    {
        private readonly IOptions<SqliteConnectionOptions> _options;

        public SqliteConnectionFactory(IOptions<SqliteConnectionOptions> options)
        {
            _options = options;
        }

        public async Task<IDbConnection> OpenAsync(OpenMode mode = OpenMode.ReadWrite)
        {
            SqliteConnection connection;
            if (mode == OpenMode.ReadWrite)
            {
                connection = new SqliteConnection(_options.Value.Master);
            }
            else if (mode == OpenMode.ReadOnly)
            {
                var index = new Random().Next(_options.Value.Slaves.Length);
                connection = new SqliteConnection(_options.Value.Slaves?[index]);
            }
            else
            {
                throw new NotImplementedException($"Unkonw OpenMode: {mode}.");
            }

            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }
    }
}
