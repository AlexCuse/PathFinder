using System;
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
        //.44074 meters/second = approximately 1 MPH 
        //this gets us very close to garmin's moving time calculation
        private double _velocityThresholdForMovingTime = .44704;

        [Test]
        public void Collect()
        {
            var start = DateTime.Now;
            var end = start.AddDays(3);

            var philadelphia = new WayPoint
            {
                Latitude = 39.9522,
                Longitude = 75.1642,
                TimeStamp = start
            };

            var sanFrancisco = new WayPoint
            {
                Latitude = 37.7750,
                Longitude = 122.4183,
                TimeStamp = end
            };

            var timer = new Timer(_velocityThresholdForMovingTime);

            timer.Collect(philadelphia, sanFrancisco);

            Assert.AreEqual((end - start).TotalSeconds, timer.TotalTime);
            Assert.AreEqual((end - start).TotalSeconds, timer.MovingTime);
            Assert.AreEqual(0, timer.StoppedTime);
        }

        [Test]
        public void Collect_Stopped()
        {
            var start = DateTime.Now;
            var end = start.AddDays(3000);

            var philadelphia = new WayPoint
            {
                Latitude = 39.9522,
                Longitude = 75.1642,
                TimeStamp = start
            };

            var sanFrancisco = new WayPoint
            {
                Latitude = 37.7750,
                Longitude = 122.4183,
                TimeStamp = end
            };

            var timer = new Timer(_velocityThresholdForMovingTime);

            timer.Collect(philadelphia, sanFrancisco);

            Assert.AreEqual((end - start).TotalSeconds, timer.TotalTime);
            Assert.AreEqual(0, timer.MovingTime);
            Assert.AreEqual((end - start).TotalSeconds, timer.StoppedTime);
        }

        [TestCase("GreenLane.gpx", 4683, 4092)]
        [TestCase("DamLoop.gpx", 13246, 12467)]
        [TestCase("WhiteClay.gpx", 12345, 9923)]
        public void Collect_Integration(string fileName, int totalTime, int movingTime)
        {
            var data = LoadData(fileName);

            var time = new Timer(_velocityThresholdForMovingTime);

            var aggregator = new Aggregator(new[] { time });
            aggregator.Aggregate(data);

            Assert.AreEqual(totalTime, time.TotalTime);
            Assert.AreEqual(movingTime, time.MovingTime);
        }

        GPSData LoadData(string fileName)
        {
            GPSData data;
            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("PathFinder.Tests.TransformationTests.SampleFiles." + fileName))
            using (var reader = new StreamReader(stream))
            {
                data = Transform.Gpx.Read(reader);
            }
            return data;
        }
    }
}
