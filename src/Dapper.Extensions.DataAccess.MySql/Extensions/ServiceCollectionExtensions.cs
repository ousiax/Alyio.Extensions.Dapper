using Dapper.Extensions.DataAccess;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationPath"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySqlDataAccess(this IServiceCollection services, string configurationPath = "dapper.xml")
        {
            services.AddRepository(configurationPath)
                .AddSingleton<IConnectionFactory, MySqlConnectionFactory>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <param name="configurationPath"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySqlDataAccess(this IServiceCollection services, Action<MySqlConnectionOptions> setupAction, string configurationPath = "dapper.xml")
        {
            services.AddMySqlDataAccess(configurationPath).Configure(setupAction);

            return services;
        }
    }
}
