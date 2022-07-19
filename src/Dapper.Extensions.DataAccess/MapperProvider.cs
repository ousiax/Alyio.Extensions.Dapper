namespace Dapper.Extensions.DataAccess
{
    internal sealed class MapperProvider<TEntity, TId> : IMapperProvider<TEntity, TId> where TEntity : IEntity<TId>, new()
    {
        public MapperProvider(IConfigurationProvider configurationProvider)
        {
            var mapper = configurationProvider.Mappers[typeof(TEntity)];
            // TODO mapper may be null.
            SelectDefinitions = mapper.SelectDefinitions.ToDictionary(k => k.Id, v => v);
            InsertDefinitions = mapper.InsertDefinitions.ToDictionary(k => k.Id, v => v);
            DeleteDefinitions = mapper.DeleteDefinitions.ToDictionary(k => k.Id, v => v);
            UpdateDefinitions = mapper.UpdateDefinitions.ToDictionary(k => k.Id, v => v);
        }

        public IDictionary<string, SelectDefinition> SelectDefinitions { get; }

        public IDictionary<string, InsertDefinition> InsertDefinitions { get; }

        public IDictionary<string, DeleteDefinition> DeleteDefinitions { get; }

        public IDictionary<string, UpdateDefinition> UpdateDefinitions { get; }
    }
}
