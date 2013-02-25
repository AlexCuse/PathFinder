using NUnit.Framework;
using PathFinder.Domain;
using PathFinder.Domain.Distance;

namespace PathFinder.Tests.Domain.Distance
{
    [TestFixture]
    public class CalculationsTest
    {
        [Test]
        public void HaversineDistanceInMeters()
        {
            var philadelphia = new WayPoint
                                     {
                                         Latitude = 39.9522,
                                         Longitude = 75.1642
                                     };

            var sanFrancisco = new WayPoint
                                    {
                                        Latitude = 37.7750,
                                        Longitude = 122.4183
                                    };

            var expectedDistance = 4050385.9129165481;

            Assert.AreEqual(expectedDistance, Calculations.HaversineDistanceInMeters(philadelphia, sanFrancisco));
            Assert.AreEqual(expectedDistance, Calculations.HaversineDistanceInMeters(sanFrancisco, philadelphia));
        }
    }
}
