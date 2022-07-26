using Alyio.Extensions.Dapper;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up MySQL generic repository services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class MySqlServiceCollectionExtensions
    {
        /// <summary>
        /// Adds MySQL generic repository services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="configurationPath">The path of dapper configuration. Default is `dapper.xml`.</param>
        /// <returns> A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddMySqlDataAccess(this IServiceCollection services, string configurationPath = "dapper.xml")
        {
            services.AddRepository(configurationPath)
                .AddSingleton<IConnectionFactory, MySqlConnectionFactory>()
                .AddScoped(typeof(IStoreService<,>), typeof(MySqlStoreService<,>));

            return services;
        }

        /// <summary>
        /// Adds MySQL generic repository services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="setupAction">The <see cref="MySqlConnectionOptions"/> configuration delegate.</param>
        /// <param name="configurationPath">The path of dapper configuration. Default is `dapper.xml`.</param>
        /// <returns> A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddMySqlDataAccess(this IServiceCollection services, Action<MySqlConnectionOptions> setupAction, string configurationPath = "dapper.xml")
        {
            services.AddMySqlDataAccess(configurationPath).Configure(setupAction);

            return services;
        }
    }
}
