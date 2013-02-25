﻿using System.IO;
using System.Reflection;
using NUnit.Framework;
using PathFinder.Domain;
using PathFinder.Domain.Distance;
using PathFinder.Transformation;

namespace PathFinder.Tests.Domain.Distance
{
    [TestFixture]
    public class OdometerTest
    {
        [TestCase("GreenLane.gpx", 14385.166705079719)]
        [TestCase("DamLoop.gpx", 93679.018730527605)]
        [TestCase("WhiteClay.gpx", 36112.637640450499)]
        public void Time(string fileName, double distanceInMeters)
        {
            var data = LoadData(fileName);

            //.44074 meters/second = approximately 1 MPH 
            //this gets us very close to garmin's moving time calculation
            var odometer = new Odometer();

            var aggregator = new Aggregator(new[] { odometer });
            aggregator.Aggregate(data);

            Assert.AreEqual(distanceInMeters, odometer.Distance);
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