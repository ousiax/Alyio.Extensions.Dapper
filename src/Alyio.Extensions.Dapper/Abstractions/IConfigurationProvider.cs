namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents a type used to provide dapper configuration.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Gets a dapper configuration.
        /// </summary>
        Configuration Configuration { get; }

        /// <summary>
        /// Gets a key/value collection that represents the definition of mapped SQL statements.
        /// </summary>
        IDictionary<Type, Mapper> Mappers { get; }
    }
}
