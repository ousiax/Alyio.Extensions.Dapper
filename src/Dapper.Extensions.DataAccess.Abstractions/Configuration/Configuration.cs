using System.Xml.Serialization;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// Represents the dapper configuration.
    /// </summary>
    [XmlRoot("configuration")]
    public sealed class Configuration
    {
        /// <summary>
        /// Gets or sets the definition of mapped SQL statements.
        /// </summary>
        [XmlArray("mappers")]
        [XmlArrayItem("mapper")]
        public List<MapperDefinition> MapperDefinitions { get; } = new List<MapperDefinition>();
    }
}
