namespace Dapper.Extensions.DataAccess
{
    internal sealed class MapperProvider<TEntity, TId> : IMapperProvider<TEntity, TId> where TEntity : class, new()
    {
        public MapperProvider(IConfigurationProvider configurationProvider)
        {
            if (configurationProvider.Mappers.TryGetValue(typeof(TEntity), out var mapper))
            {
                SelectDefinitions = mapper.SelectDefinitions.ToDictionary(k => k.Id, v => v);
                InsertDefinitions = mapper.InsertDefinitions.ToDictionary(k => k.Id, v => v);
                DeleteDefinitions = mapper.DeleteDefinitions.ToDictionary(k => k.Id, v => v);
                UpdateDefinitions = mapper.UpdateDefinitions.ToDictionary(k => k.Id, v => v);
            }
            else
            {
                throw new ArgumentException($"The mapper of type `{typeof(TEntity)}` was not defined, please check dapper configuration.");
            }
        }

        public IDictionary<string, SelectDefinition> SelectDefinitions { get; }

        public IDictionary<string, InsertDefinition> InsertDefinitions { get; }

        public IDictionary<string, DeleteDefinition> DeleteDefinitions { get; }

        public IDictionary<string, UpdateDefinition> UpdateDefinitions { get; }
    }
}
