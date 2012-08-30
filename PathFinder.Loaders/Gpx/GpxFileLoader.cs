using System;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using PathFinder.Domain;

namespace PathFinder.Loaders.Gpx
{
    public sealed class GpxFileLoader : IFileLoader
    {
        static readonly Lazy<XmlSchemaSet> _schemaSet = new Lazy<XmlSchemaSet>(ReadXmlSchemaSet);

        public PathCollection Load(string filePath)
        {
            var doc = XDocument.Load(filePath);
            doc.Validate(_schemaSet.Value, null);
            var serializer = new XmlSerializer(typeof(gpxType));
            var gpx = (gpxType)serializer.Deserialize(doc.CreateReader());

            throw new NotImplementedException();
        }

        static XmlSchemaSet ReadXmlSchemaSet()
        {
            var set = new XmlSchemaSet();
            var assembly = Assembly.GetAssembly(typeof(GpxFileLoader));
            using (var stream = assembly.GetManifestResourceStream("PathFinder.Loaders.Gpx.Gpx.xsd"))
            {
                var schema = XmlSchema.Read(stream, null);
                set.Add(schema);
                return set;
            }
        }
    }
}