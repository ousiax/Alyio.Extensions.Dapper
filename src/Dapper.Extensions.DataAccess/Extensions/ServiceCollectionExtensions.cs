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
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationProvider, ConfigurationProvider>()
                .AddSingleton(typeof(IMapperProvider<,>), typeof(MapperProvider<,>))
                .AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            return services;
        }
    }
}
