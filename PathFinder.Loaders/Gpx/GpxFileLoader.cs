using System;
using System.IO;
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

        public GPSData Load(string filePath)
        {
            var output = new GPSData();

            var gpx = LoadGpx(filePath);

            foreach(var track in gpx.trk)
            {
                var way = new Way();
                foreach(var trackSegment in track.trkseg)
                {
                    foreach(var trackPoint in trackSegment.trkpt)
                    {
                        way.Add(new WayPoint
                                    {
                                        Latitude = trackPoint.lat,
                                        Longitude = trackPoint.lon,
                                        Elevation = trackPoint.eleSpecified ? (decimal?)trackPoint.ele : null
                                    });
                    }
                }
                output.Add(way);
            }
            return output;
        }

        private static gpxType LoadGpx(string filePath)
        {
            //var doc = XDocument.Load(filePath);
            //doc.Validate(_schemaSet.Value, null);
            var serializer = new XmlSerializer(typeof(gpxType), "http://www.topografix.com/GPX/1/1");
            
            using (var reader = new FileStream(filePath, FileMode.Open))
            {
                return (gpxType) serializer.Deserialize(reader);
            }
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