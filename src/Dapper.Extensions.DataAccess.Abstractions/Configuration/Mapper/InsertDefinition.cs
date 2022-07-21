using System.Xml.Serialization;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// A mapped INSERT statement.
    /// </summary>
    [XmlRoot("insert")]
    public sealed class InsertDefinition : BaseDefinition
    {
    }
}
