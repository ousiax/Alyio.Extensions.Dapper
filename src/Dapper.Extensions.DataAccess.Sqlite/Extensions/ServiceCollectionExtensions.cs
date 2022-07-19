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
        /// <returns></returns>
        public static IServiceCollection AddSqliteDataAccess(this IServiceCollection services)
        {
            services.AddRepository()
                .AddSingleton<IDbConnectionFactory, SqliteConnectionFactory>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqliteDataAccess(this IServiceCollection services, Action<SqliteConnectionOptions> setupAction)
        {
            services.AddSqliteDataAccess().Configure(setupAction);

            return services;
        }
    }
}
