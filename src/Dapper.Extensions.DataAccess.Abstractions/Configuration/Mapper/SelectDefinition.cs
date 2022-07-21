using System.Xml.Serialization;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// A mapped SELECT statement. 
    /// </summary>
    [XmlRoot("select")]
    public sealed class SelectDefinition : BaseDefinition
    {
        /// <summary>
        /// Gets or sets the primary key column name of the table.
        /// </summary>
        [XmlAttribute("idName")]
        public string? IdName { get; set; }
    }
}
