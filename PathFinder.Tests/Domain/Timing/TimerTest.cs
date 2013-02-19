using System.IO;
using System.Reflection;
using NUnit.Framework;
using PathFinder.Domain;
using PathFinder.Domain.Timing;
using PathFinder.Transformation;

namespace PathFinder.Tests.Domain.Timing
{
    [TestFixture]
    public class TimerTest
    {
        [TestCase("GreenLane.gpx", 4683, 4092)]
        [TestCase("DamLoop.gpx", 13246, 12467)]
        [TestCase("WhiteClay.gpx", 12345, 9923)]
        public void Time(string fileName, int totalTime, int movingTime)
        {
            var data = LoadData(fileName);

            //.44074 meters/second = approximately 1 MPH 
            //this gets us very close to garmin's moving time calculation
            var time = new Timer(.44704).Time(data);

            Assert.AreEqual(totalTime, time.TotalTime);
            Assert.AreEqual(movingTime, time.MovingTime);
        }

        GPSData LoadData(string fileName)
        {
            GPSData data;
            using(var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("PathFinder.Tests.TransformationTests.SampleFiles." + fileName))
            using (var reader = new StreamReader(stream))
            {
                data = Transform.Gpx.Read(reader);
            }
            return data;
        }
    }
}
