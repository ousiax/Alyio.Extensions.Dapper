using System.Data;
using System.Xml.Serialization;

namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// Represents an SQL statement that is executed while connected to a data source, and is implemented by .NET data providers that access relational databases.
    /// </summary>
    public class BaseDefinition
    {
        /// <summary>
        /// Gets or sets a unique identifier that can be used to reference this statement.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Gets or sets the text command to run against the data source.
        /// </summary>
        [XmlText]
        public string CommandText { get; set; } = null!;

        /// <summary>
        /// Gets or sets the wait time (in seconds) before terminating the attempt to execute a command and generating an error. The default value is 30 seconds.
        /// </summary>
        [XmlAttribute("commandTimeout")]
        public int CommandTimeout { get; set; } = 30;

        /// <summary>
        /// Indicates or specifies how the CommandText property is interpreted. The default is Text.
        /// </summary>
        /// <remarks>
        /// When you set the CommandType property to StoredProcedure, you should set the CommandText property to the name of the stored procedure.
        /// The command executes this stored procedure when you call one of the Execute methods.
        /// </remarks>
        [XmlAttribute("commandType")]
        public CommandType CommandType { get; set; } = CommandType.Text;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, CommandText: {CommandText}";
        }
    }
}