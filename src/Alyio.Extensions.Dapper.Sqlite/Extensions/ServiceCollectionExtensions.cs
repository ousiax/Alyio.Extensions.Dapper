using Alyio.Extensions.Dapper;

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
        public static IServiceCollection AddSqliteDataAccess(this IServiceCollection services, string configurationPath = "dapper.xml")
        {
            services.AddRepository(configurationPath)
                .AddSingleton<IConnectionFactory, SqliteConnectionFactory>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <param name="configurationPath"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqliteDataAccess(this IServiceCollection services, Action<SqliteConnectionOptions> setupAction, string configurationPath = "dapper.xml")
        {
            services.AddSqliteDataAccess(configurationPath).Configure(setupAction);

            return services;
        }
    }
}
