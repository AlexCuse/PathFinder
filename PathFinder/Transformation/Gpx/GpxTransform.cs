using PathFinder.Domain;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using PathFinder.Utilities;

namespace PathFinder.Transformation.Gpx
{
    internal sealed class GpxTransform : ITextTransform
    {
        static readonly Lazy<XmlSchemaSet> _schemaSet = new Lazy<XmlSchemaSet>(ReadXmlSchemaSet);

        public GPSData Read(TextReader reader)
        {
            reader.ThrowIfNull("reader");

            var data = new GPSData();
            var gpx = LoadGpx(reader);
            foreach (var track in gpx.trk)
            {
                var way = new Way();
                foreach (var trackSegment in track.trkseg)
                {
                    foreach (var trackPoint in trackSegment.trkpt)
                    {
                        way.Add(new WayPoint
                        {
                            Latitude = Convert.ToDouble(trackPoint.lat),
                            Longitude = Convert.ToDouble(trackPoint.lon),
                            Elevation = trackPoint.eleSpecified ? (double?)trackPoint.ele : null,
                            TimeStamp = trackPoint.timeSpecified ? (DateTime?)trackPoint.time : null
                        });
                    }
                }
                data.Add(way);
            }
            return data;
        }

        public void Write(TextWriter writer, GPSData data)
        {
            throw new NotImplementedException();
        }

        static gpxType LoadGpx(TextReader textReader)
        {
            var doc = XDocument.Load(textReader);
            doc.Validate(_schemaSet.Value, null);
            var serializer = new XmlSerializer(typeof(gpxType), "http://www.topografix.com/GPX/1/1");
            using (var reader = doc.CreateReader())
            {
                return (gpxType) serializer.Deserialize(reader);
            }
        }

        static XmlSchemaSet ReadXmlSchemaSet()
        {
            var set = new XmlSchemaSet();
            var assembly = Assembly.GetAssembly(typeof(GpxTransform));
            using (var stream = assembly.GetManifestResourceStream("PathFinder.Transformation.Gpx.Gpx.xsd"))
            {
                var schema = XmlSchema.Read(stream, null);
                set.Add(schema);
                return set;
            }
        }
    }
}