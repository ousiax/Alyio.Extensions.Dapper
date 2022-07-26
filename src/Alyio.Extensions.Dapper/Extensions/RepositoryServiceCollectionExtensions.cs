using Alyio.Extensions.Dapper;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up generic repository services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class RepositoryServiceCollectionExtensions
    {
        /// <summary>
        /// Adds generic repository services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="configurationPath">The path of dapper configuration.</param>
        /// <returns> A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddRepository(this IServiceCollection services, string configurationPath)
        {
            if (configurationPath == null)
            {
                throw new ArgumentNullException(nameof(configurationPath));
            }

            services.AddSingleton(typeof(IConfigurationProvider), new ConfigurationProvider(configurationPath));
            services.AddSingleton(typeof(IMapperDefinitionProvider<,>), typeof(MapperDefinitionProvider<,>))
                .AddScoped(typeof(IStoreService<,>), typeof(StoreService<,>))
                .AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            return services;
        }
    }
}
