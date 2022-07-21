using System.Xml.Serialization;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// A mapped DELETE statement. 
    /// </summary>
    [XmlRoot("delete")]
    public sealed class DeleteDefinition : BaseDefinition
    {
        /// <summary>
        /// Gets or sets the primary key column name of the table.
        /// </summary>
        [XmlAttribute("idName")]
        public string? IdName { get; set; }
    }
}
