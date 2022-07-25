namespace Alyio.Extensions.Dapper
{
    internal sealed class MapperDefinitionProvider<TEntity, TId> : IMapperDefinitionProvider<TEntity, TId> where TEntity : class, new()
    {
        private IDictionary<string, SelectDefinition> SelectDefinitions { get; }

        private IDictionary<string, InsertDefinition> InsertDefinitions { get; }

        private IDictionary<string, DeleteDefinition> DeleteDefinitions { get; }

        private IDictionary<string, UpdateDefinition> UpdateDefinitions { get; }

        public MapperDefinitionProvider(IConfigurationProvider configurationProvider)
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

        public bool TryFindSelect(string id, out SelectDefinition definition)
        {
            return SelectDefinitions.TryGetValue(id, out definition);
        }

        public bool TryFindInsert(string id, out InsertDefinition definition)
        {
            return InsertDefinitions.TryGetValue(id, out definition);
        }

        public bool TryFindDelete(string id, out DeleteDefinition definition)
        {
            return DeleteDefinitions.TryGetValue(id, out definition);
        }

        public bool TryFindUpdate(string id, out UpdateDefinition definition)
        {
            return UpdateDefinitions.TryGetValue(id, out definition);
        }
    }
}
