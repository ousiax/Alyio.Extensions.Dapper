namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents the connection modes that can be used when opening a connection.
    /// </summary>
    public enum OpenMode
    {
        /// <summary>
        /// Opens the database for reading and writing.
        /// </summary>
        ReadWrite = 0,

        /// <summary>
        /// Opens the database in read-only mode.
        /// </summary>
        ReadOnly = 1,
    }
}