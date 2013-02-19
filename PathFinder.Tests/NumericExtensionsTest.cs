using NUnit.Framework;

namespace PathFinder.Tests
{
    [TestFixture]
    public class NumericExtensionsTest
    {
        [TestCase(90, 1.5707963267948966)]
        [TestCase(180, 3.1415926535897931)]
        [TestCase(360, 6.2831853071795862)]
        public void ToRadians(double degrees, double expectedRadians)
        {
            Assert.AreEqual(expectedRadians, degrees.ToRadians());
        }
    }
}
