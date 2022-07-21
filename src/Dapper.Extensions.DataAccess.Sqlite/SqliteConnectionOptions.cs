namespace Dapper.Extensions.DataAccess
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
        public IList<string>? Slaves { get; set; }
    }
}
