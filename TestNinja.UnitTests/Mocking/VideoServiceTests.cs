using System;
using System.Linq;
using System.Collections.Generic;
using TestNinja.Mocking;
using NUnit.Framework;
using Moq;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;
        private VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            Assert.That(_videoService.ReadVideoTitle(), Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCSV_NoUnprocessedVideos_ReturnAnEmptyString()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCSV_AFewUnprocessedVideos_ReturnStringWithUnprocessedVideoIds()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video> { 
                new Video() {Id = 1},
                new Video() {Id = 2},
                new Video() {Id = 3},
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
