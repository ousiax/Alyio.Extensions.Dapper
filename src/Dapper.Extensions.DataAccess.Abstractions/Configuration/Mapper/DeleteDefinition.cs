using System.Xml.Serialization;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// A mapped DELETE statement. 
    /// </summary>
    [XmlRoot("delete")]
    public sealed class DeleteDefinition
    {
        /// <summary>
        /// A unique identifier that can be used to reference this statement.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("idName")]
        public string? IdName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlText]
        public string Sql { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, Sql: {Sql}";
        }
    }
}
