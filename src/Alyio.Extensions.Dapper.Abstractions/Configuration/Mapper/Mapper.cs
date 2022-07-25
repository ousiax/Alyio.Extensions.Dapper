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
        /// 
        /// </summary>
        [XmlElement("select")]
        public List<SelectDefinition> SelectDefinitions { get; } = new List<SelectDefinition>();

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("insert")]
        public List<InsertDefinition> InsertDefinitions { get; } = new List<InsertDefinition>();

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("delete")]
        public List<DeleteDefinition> DeleteDefinitions { get; } = new List<DeleteDefinition>();

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("update")]
        public List<UpdateDefinition> UpdateDefinitions { get; } = new List<UpdateDefinition>();
    }
}
