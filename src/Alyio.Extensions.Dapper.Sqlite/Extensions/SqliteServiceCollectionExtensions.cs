using Alyio.Extensions.Dapper;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up SQLite generic repository services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class SqliteServiceCollectionExtensions
    {
        /// <summary>
        /// Adds SQLite generic repository services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="configurationPath">The path of dapper configuration. Default is `dapper.xml`.</param>
        /// <returns> A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddSqliteStore(this IServiceCollection services, string configurationPath = "dapper.xml")
        {
            services.AddDapper(configurationPath)
                .AddSingleton<IConnectionFactory, SqliteConnectionFactory>()
                .AddScoped(typeof(IStoreService<,>), typeof(SqliteStoreService<,>));

            return services;
        }

        /// <summary>
        /// Adds SQLite generic repository services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="setupAction">The <see cref="SqliteConnectionOptions"/> configuration delegate.</param>
        /// <param name="configurationPath">The path of dapper configuration. Default is `dapper.xml`.</param>
        /// <returns> A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddSqliteStore(this IServiceCollection services, Action<SqliteConnectionOptions> setupAction, string configurationPath = "dapper.xml")
        {
            services.AddSqliteStore(configurationPath).Configure(setupAction);

            return services;
        }
    }
}
