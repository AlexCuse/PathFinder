﻿using PathFinder.Domain;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PathFinder.Transformation.Gpx
{
    public sealed class GpxTransform : ITransform
    {
        static readonly Lazy<XmlSchemaSet> _schemaSet = new Lazy<XmlSchemaSet>(ReadXmlSchemaSet);

        public GPSData TransformInput(TextReader textReader)
        {
            var data = new GPSData();
            var gpx = LoadGpx(textReader);
            foreach (var track in gpx.trk)
            {
                var way = new Way();
                foreach (var trackSegment in track.trkseg)
                {
                    foreach (var trackPoint in trackSegment.trkpt)
                    {
                        way.Add(new WayPoint
                        {
                            Latitude = trackPoint.lat,
                            Longitude = trackPoint.lon,
                            Elevation = trackPoint.eleSpecified ? (decimal?)trackPoint.ele : null
                        });
                    }
                }
                data.Add(way);
            }
            return data;
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