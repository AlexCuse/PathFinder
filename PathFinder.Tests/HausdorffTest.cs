using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace PathFinder.Tests
{
    [TestFixture]
    public class HausdorffTest
    {
        //TODO: investigate using NTS' DiscreteHausdorffDistance - 
        //http://code.google.com/p/nettopologysuite/source/browse/trunk/NetTopologySuite/Algorithm/Distance/DiscreteHausdorffDistance.cs
        [Test]
        public void Distance_CoordinateCollections()
        {
            var a = new[]
                        {
                            new Coordinate(5, 10),
                            new Coordinate(19, 11),
                            new Coordinate(6, 10.5), 
                        };

            var b = new[]
                        {
                            new Coordinate(16, 35),
                            new Coordinate(22, 57),
                            new Coordinate(122, 213), 
                        };

            var distance = Hausdorff.Distance(a, b);

            Assert.AreEqual(27.313000567495326, distance);
        }

        //TODO: setup test that triggers "further investigation" and implement
        [Test]
        public void Distance_Lines()
        {
            var a = new LineString(new[]
                        {
                            new Coordinate(5, 10),
                            new Coordinate(19, 11),
                            new Coordinate(6, 10.5), 
                        });

            var b = new LineString(new[]
                        {
                            new Coordinate(16, 35),
                            new Coordinate(22, 57),
                            new Coordinate(122, 213), 
                            new Coordinate(574, 112), 
                        });

            var distance = Hausdorff.Distance(a, b);

            Assert.AreEqual(27.313000567495326, distance);
        }
    }
}
