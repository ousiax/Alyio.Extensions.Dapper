﻿using System.Data;

using Microsoft.Extensions.Options;

using MySqlConnector;

namespace Alyio.Extensions.Dapper
{
    internal sealed class MySqlConnectionFactory : IConnectionFactory
    {
        private readonly IOptions<MySqlConnectionOptions> _options;

        public MySqlConnectionFactory(IOptions<MySqlConnectionOptions> options)
        {
            _options = options;
        }

        public async Task<IDbConnection> OpenAsync(OpenMode mode = OpenMode.ReadWrite)
        {
            MySqlConnection connection;
            if (mode == OpenMode.ReadWrite)
            {
                connection = new MySqlConnection(_options.Value.Master);
            }
            else if (mode == OpenMode.ReadOnly)
            {
                var index = new Random().Next(_options.Value.Slaves.Length);
                connection = new MySqlConnection(_options.Value.Slaves?[index]);
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
