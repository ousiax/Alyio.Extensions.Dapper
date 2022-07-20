namespace Dapper.Extensions.DataAccess
{
    internal sealed class MapperProvider<TEntity, TId> : IMapperProvider<TEntity, TId> where TEntity : class, new()
    {
        private IDictionary<string, SelectDefinition> SelectDefinitions { get; }

        private IDictionary<string, InsertDefinition> InsertDefinitions { get; }

        private IDictionary<string, DeleteDefinition> DeleteDefinitions { get; }

        private IDictionary<string, UpdateDefinition> UpdateDefinitions { get; }

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

        public bool TryFindSelectDefinition(string id, out SelectDefinition definition)
        {
            return SelectDefinitions.TryGetValue(id, out definition);
        }

        public bool TryFindInsertDefinition(string id, out InsertDefinition definition)
        {
            return InsertDefinitions.TryGetValue(id, out definition);
        }

        public bool TryFindDeleteDefinition(string id, out DeleteDefinition definition)
        {
            return DeleteDefinitions.TryGetValue(id, out definition);
        }

        public bool TryFindUpdateDefinition(string id, out UpdateDefinition definition)
        {
            return UpdateDefinitions.TryGetValue(id, out definition);
        }
    }
}
