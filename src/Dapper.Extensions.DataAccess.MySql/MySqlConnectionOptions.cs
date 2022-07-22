namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class MySqlConnectionOptions
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
