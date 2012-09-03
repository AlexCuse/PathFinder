using System;
using NUnit.Framework;
using System.Linq;
using PathFinder.Transformation;
using PathFinder.Transformation.Gpx;

namespace PathFinder.Loaders.Tests.Gpx
{
    [TestFixture]
    public class GpxFileLoaderTest
    {
        [TestCase("SampleFiles\\DamLoop.gpx", 1, 1155)]
        [TestCase("SampleFiles\\GreenLane.gpx", 1, 1665)]//with garmin extensions
        public void Load(string filePath, int wayCount, int waypointCount)
        {
            var transform = new GpxTransform();

            var gpsData = transform.TransformFileInput(filePath);
            Assert.AreEqual(wayCount, gpsData.Count);
            Assert.AreEqual(waypointCount, gpsData[0].Count);
            Assert.True(gpsData[0].All(wp => Math.Abs(wp.Latitude) > 0 && Math.Abs(wp.Longitude) > 0 && wp.Elevation > 0));
        }
    }
}
