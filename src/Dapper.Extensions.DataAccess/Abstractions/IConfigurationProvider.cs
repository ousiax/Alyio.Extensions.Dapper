namespace Dapper.Extensions.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// 
        /// </summary>
        Configuration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        IDictionary<Type, Mapper> Mappers { get; }
    }
}
