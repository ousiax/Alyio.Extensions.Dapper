﻿namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// 
    /// </summary>
    public class SqliteConnectionOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Master { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] Slaves { get; set; } = Array.Empty<string>();
    }
}