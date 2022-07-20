using System.Xml.Serialization;

using Microsoft.Extensions.Hosting;

namespace Dapper.Extensions.DataAccess
{
    internal sealed class ConfigurationProvider : IConfigurationProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5369:Use XmlReader for 'XmlSerializer.Deserialize()'", Justification = "<Pending>")]
        public ConfigurationProvider(IHostingEnvironment hostingEnvironment)
        {
            var configPath = Path.Combine(hostingEnvironment.ContentRootPath, "dapper.xml");
            using var configXml = new FileStream(configPath, FileMode.Open);

            Configuration = (Configuration)new XmlSerializer(typeof(Configuration)).Deserialize(configXml);

            Mappers = new Dictionary<Type, Mapper>();
            foreach (var mapperDefinition in Configuration.MapperDefinitions)
            {
                var type = Type.GetType(mapperDefinition.Type, true);
                using var resource = type.Assembly.GetManifestResourceStream(mapperDefinition.Name);
                if (resource == null)
                {
                    throw new ArgumentException($"Get manifest resource of type `{mapperDefinition.Type}` with name `{mapperDefinition.Name}`, please check if the specified resource has been embeded.");
                }
                var xmlMapper = new XmlSerializer(typeof(Mapper));
                var mapper = (Mapper)xmlMapper.Deserialize(resource);
                Mappers.Add(type, mapper);
            }
        }

        public Configuration Configuration { get; }

        public IDictionary<Type, Mapper> Mappers { get; }
    }
}
