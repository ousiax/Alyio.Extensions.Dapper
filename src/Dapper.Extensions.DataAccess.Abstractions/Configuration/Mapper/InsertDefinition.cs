using System.Xml.Serialization;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// A mapped INSERT statement.
    /// </summary>
    [XmlRoot("insert")]
    public sealed class InsertDefinition
    {
        /// <summary>
        /// A unique identifier that can be used to reference this statement.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; } = null!;

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
