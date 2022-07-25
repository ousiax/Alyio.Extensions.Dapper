using System.Xml.Serialization;

namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Represents the definition of mapped SQL statements.
    /// </summary>
    [XmlRoot("mapper")]
    public sealed class Mapper
    {
        /// <summary>
        /// Gets a list of <see cref="SelectDefinition"/>.
        /// </summary>
        [XmlElement("select")]
        public List<SelectDefinition> SelectDefinitions { get; } = new List<SelectDefinition>();

        /// <summary>
        /// Gets a list of <see cref="InsertDefinition"/>.
        /// </summary>
        [XmlElement("insert")]
        public List<InsertDefinition> InsertDefinitions { get; } = new List<InsertDefinition>();

        /// <summary>
        /// Gets a list of <see cref="DeleteDefinition"/>.
        /// </summary>
        [XmlElement("delete")]
        public List<DeleteDefinition> DeleteDefinitions { get; } = new List<DeleteDefinition>();

        /// <summary>
        /// Gets a list of <see cref="UpdateDefinition"/>.
        /// </summary>
        [XmlElement("update")]
        public List<UpdateDefinition> UpdateDefinitions { get; } = new List<UpdateDefinition>();
    }
}
