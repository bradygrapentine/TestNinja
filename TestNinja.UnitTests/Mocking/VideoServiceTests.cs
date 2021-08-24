using System;
using TestNinja.Mocking;
using NUnit.Framework;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var service = new VideoService(new FakeFileReader());
            Assert.That(service.ReadVideoTitle(), Does.Contain("error").IgnoreCase);
        }
    }
}
