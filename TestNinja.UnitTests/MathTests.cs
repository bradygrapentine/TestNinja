using NUnit.Framework;
using TestNinja.Fundamentals;
using System.Linq;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }

        [Test]
//        [Ignore("cuz y not")]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {

            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_Return_Max(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);
            Assert.That(result == expectedResult);
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_returnOddNumbersUpToLimit()
        {
           var result =  _math.GetOddNumbers(5);

 //           Assert.That(result, Is.Not.Empty);
//            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }
    }
}
