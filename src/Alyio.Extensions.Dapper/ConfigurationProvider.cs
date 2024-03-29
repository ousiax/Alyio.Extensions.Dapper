﻿using System.Xml.Serialization;

namespace Alyio.Extensions.Dapper
{
    internal sealed class ConfigurationProvider : IConfigurationProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5369:Use XmlReader for 'XmlSerializer.Deserialize()'", Justification = "<Pending>")]
        public ConfigurationProvider(string configurationPath)
        {
            using var configXml = new FileStream(configurationPath, FileMode.Open);

            Configuration = (Configuration)new XmlSerializer(typeof(Configuration)).Deserialize(configXml);

            Mappers = new Dictionary<Type, Mapper>();
            foreach (var mapperDefinition in Configuration.MapperDefinitions)
            {
                var type = Type.GetType(mapperDefinition.Type, true);
                var name = mapperDefinition.Name ?? $"{type.Namespace}.{type.Name}.xml";
                using var resource = type.Assembly.GetManifestResourceStream(name) ?? throw new ArgumentException($"Couldn't get manifest resource of type `{mapperDefinition.Type}` with name `{name}`.");
                var xmlMapper = new XmlSerializer(typeof(Mapper));
                var mapper = (Mapper)xmlMapper.Deserialize(resource);
                Mappers.Add(type, mapper);
            }
        }

        public Configuration Configuration { get; }

        public IDictionary<Type, Mapper> Mappers { get; }
    }
}
