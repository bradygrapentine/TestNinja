using System;
using TestNinja.Fundamentals;
using NUnit.Framework;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void GetOuput_DivisibleBy3And5_ReturnFizzBuzz()
        {
            Assert.That(FizzBuzz.GetOutput(15), Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOuput_DivisibleBy3_ReturnFizz()
        {
            Assert.That(FizzBuzz.GetOutput(3), Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOuput_DivisibleBy5_ReturnBuzz()
        {
            Assert.That(FizzBuzz.GetOutput(5), Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOuput_NotDivisibleBy3Or5_ReturnInput()
        {
            Assert.That(FizzBuzz.GetOutput(7), Is.EqualTo(7.ToString()));
        }
    }
}
