using NUnit.Framework;
using PathFinder.Loaders.Gpx;

namespace PathFinder.Loaders.Tests.Gpx
{
    [TestFixture]
    public class GpxFileLoaderTest
    {
        [TestCase("SampleFiles\\DamLoop.gpx", 1, 1155)]
        [TestCase("SampleFiles\\GreenLane.gpx", 1, 1665)]//with garmin extensions
        public void Load(string filePath, int trackCount, int trackpointCount)
        {
            var loader = new GpxFileLoader();

            var gpsData = loader.Load(filePath);
            Assert.AreEqual(trackCount, gpsData.Count);
            Assert.AreEqual(trackpointCount, gpsData[0].Count);
        }
    }
}
