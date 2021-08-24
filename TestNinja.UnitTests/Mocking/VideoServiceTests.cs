using System;
using TestNinja.Mocking;
using NUnit.Framework;
using Moq;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var mockFileReader = new Mock<IFileReader>(); // mock object via Moq
            mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            var service = new VideoService(mockFileReader.Object);
            Assert.That(service.ReadVideoTitle(), Does.Contain("error").IgnoreCase);
        }
    }
}
