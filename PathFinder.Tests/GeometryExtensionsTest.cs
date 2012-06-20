using System.Linq;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace PathFinder.Tests
{
    [TestFixture]
    public class GeometryExtensionsTest
    {

        Coordinate p0 = new Coordinate(5, 10);
        Coordinate p1 = new Coordinate(19, 11);
        Coordinate p2 = new Coordinate(6, 10.5);
        Coordinate p3 = new Coordinate(12, 100);

        [Test]
        public void Segments()
        {
            var a = new LineString(new[] { p0, p1, p2, p3 });

            var segments = a.Segments().ToList();

            Assert.AreEqual(3, segments.Count);
            AssertLineSegment(segments[0], p0, p1);
            AssertLineSegment(segments[1], p1, p2);
            AssertLineSegment(segments[2], p2, p3);
        }

        [Test]
        public void Points()
        {
            var a = new LineString(new[] { p0, p1, p2, p3 });

            var points = a.Points().ToList();

            Assert.AreEqual(4, points.Count);
            AssertCoordinates(p0, points[0]);
            AssertCoordinates(p1, points[1]);
            AssertCoordinates(p2, points[2]);
            AssertCoordinates(p3, points[3]);
        }

        void AssertLineSegment(LineString segment, Coordinate expectedStart, Coordinate expectedEnd)
        {
            Assert.AreEqual(2, segment.NumPoints);
            AssertCoordinates(expectedStart, segment.StartPoint);
            AssertCoordinates(expectedEnd, segment.EndPoint);
        }

        void AssertCoordinates(Coordinate expected, IPoint actual)
        {
            Assert.AreEqual(expected.X, actual.X);
            Assert.AreEqual(expected.Y, actual.Y);
        }
    }
}
