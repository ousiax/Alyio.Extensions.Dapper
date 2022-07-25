using System.Xml.Serialization;

namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// A mapped INSERT statement.
    /// </summary>
    [XmlRoot("insert")]
    public sealed class InsertDefinition : BaseDefinition
    {
    }
}
