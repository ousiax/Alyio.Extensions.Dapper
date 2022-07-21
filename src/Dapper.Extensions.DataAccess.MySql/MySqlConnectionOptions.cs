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
        public IList<string>? Slave { get; set; }
    }
}
