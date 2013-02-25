using Moq;
using NUnit.Framework;
using PathFinder.Domain;

namespace PathFinder.Tests.Domain
{
    [TestFixture]
    public class AggregatorTest
    {
        [Test]
        public void Aggregate()
        {
            var c1 = new Mock<IDataCollector>();
            var c2 = new Mock<IDataCollector>();

            var data = new GPSData
                {
                    new Way()
                        {
                            new WayPoint(),
                            new WayPoint(),
                            new WayPoint(),
                            new WayPoint(),
                            new WayPoint()
                        }
                };

            var aggregator = new Aggregator(new[] { c1.Object, c2.Object });

            aggregator.Aggregate(data);

            c1.Verify(c => c.Collect(It.IsAny<WayPoint>(), It.IsAny<WayPoint>()), Times.Exactly(4));
            c2.Verify(c => c.Collect(It.IsAny<WayPoint>(), It.IsAny<WayPoint>()), Times.Exactly(4));
        }
    }
}