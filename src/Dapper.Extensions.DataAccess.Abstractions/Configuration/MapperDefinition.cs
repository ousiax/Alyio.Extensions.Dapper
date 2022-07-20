using System.Xml.Serialization;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// Represents the definition of mapped SQL statements.
    /// </summary>
    [XmlRoot("mapper")]
    public sealed class MapperDefinition
    {
        /// <summary>
        /// Gets or sets the type whose namespace is used to scope the manifest resource name.
        /// </summary>
        /// <remarks><see cref="Type.AssemblyQualifiedName"/>.</remarks>
        [XmlAttribute("type")]
        public string Type { get; set; } = null!;

        /// <summary>
        /// Gets or sets the case-sensitive name of the manifest resource being requested.
        /// </summary>
        /// <remarks>If not provided, it will be built with {Type.Namespace}.{Type.Name}.xml.</remarks>
        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
