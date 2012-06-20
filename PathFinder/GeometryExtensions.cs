using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace PathFinder
{
    public static class GeometryExtensions
    {
        public static IEnumerable<LineString> Segments(this LineString line)
        {
            return Enumerable.Range(0, line.NumPoints - 1)
                .Select(i => new LineString(new [] { line[i], line[i + 1] }));
        }

        public static IEnumerable<IPoint> Points(this LineString line)
        {
            return Enumerable.Range(0, line.NumPoints)
                .Select(line.GetPointN);
        }
    }
}
