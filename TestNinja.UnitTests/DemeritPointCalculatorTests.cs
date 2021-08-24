using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
namespace TestNinja.UnitTests
{
    [TestFixture]
    public class DemeritPointCalculatorTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowArgumentOutOfRangeException(int speed)
        {
            var demeriter = new DemeritPointsCalculator();
            Assert.That(() => demeriter.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(0,64)]
        [TestCase(0,65)]
        [TestCase(0,66)]
        [TestCase(2, 75)]
        [TestCase(1, 70)]

        public void CalculateDemeritPoints_WhenCalled_DemeritPoints(int expectedResult, int speed) 
        {
            var demeriter = new DemeritPointsCalculator();
            Assert.That(demeriter.CalculateDemeritPoints(speed), Is.EqualTo(expectedResult));
        }
    }
}
