using Moq;
using NUnit.Framework;
using TestNinja.Mocking;
using System.Net;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloaFails_ReturnFalse()
        {
            _fileDownloader.Setup(f => f.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            Assert.That(_installerHelper.DownloadInstaller("customer", "installer"), Is.False);
        }


        [Test]
        public void DownloadInstaller_DownloaCompletes_ReturnTrue()
        {
            Assert.That(_installerHelper.DownloadInstaller("customer", "installer"), Is.True);
        }
    }
}
