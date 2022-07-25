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
        /// <param name="configurationPath">The path of dapper configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddRepository(this IServiceCollection services, string configurationPath)
        {
            if (configurationPath == null)
            {
                throw new ArgumentNullException(nameof(configurationPath));
            }

            services.AddSingleton(typeof(IConfigurationProvider), new ConfigurationProvider(configurationPath));
            services.AddSingleton(typeof(IMapperDefinitionProvider<,>), typeof(MapperDefinitionProvider<,>))
                .AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            return services;
        }
    }
}
