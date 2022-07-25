using System.Xml.Serialization;

namespace Alyio.Extensions.Dapper
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
