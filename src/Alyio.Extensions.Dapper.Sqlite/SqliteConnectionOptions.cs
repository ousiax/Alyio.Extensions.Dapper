﻿namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Provides programmatic configuration for the Sqlite connection strings.
    /// </summary>
    public class SqliteConnectionOptions
    {
        /// <summary>
        /// Gets or sets connection string of the master (i.e. readable and writable) server instance.
        /// </summary>
        public string? Master { get; set; }

        /// <summary>
        /// Gets or sets connection strings of the slave (i.e. readonly) server instances.
        /// </summary>
        public string[] Slaves { get; set; } = Array.Empty<string>();
    }
}
