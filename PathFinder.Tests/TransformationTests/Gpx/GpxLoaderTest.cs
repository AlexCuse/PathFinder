using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using System.Linq;
using PathFinder.Transformation.Gpx;

namespace PathFinder.Tests.TransformationTests.Gpx
{
    [TestFixture, Category("Integration")]
    public class GpxFileLoaderTest
    {
        [TestCase("DamLoop.gpx", 1, 1155)]
        [TestCase("GreenLane.gpx", 1, 1665)]//with garmin extensions
        public void Load(string filePath, int wayCount, int waypointCount)
        {
            var transform = new GpxTransform();

            using(var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("PathFinder.Tests.TransformationTests.SampleFiles." + filePath))
            using(var reader = new StreamReader(stream))
            {
                var gpsData = transform.TransformInput(reader);
                Assert.AreEqual(wayCount, gpsData.Count);
                Assert.AreEqual(waypointCount, gpsData[0].Count);
                Assert.True(gpsData[0].All(wp => Math.Abs(wp.Latitude) > 0 && Math.Abs(wp.Longitude) > 0 && wp.Elevation > 0));
            }
        }
    }
}
