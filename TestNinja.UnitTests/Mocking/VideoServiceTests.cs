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
            var service = new VideoService();
            Assert.That(service.ReadVideoTitle(new FakeFileReader()), Does.Contain("error").IgnoreCase);
        }
    }
}
