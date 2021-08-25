using System.Net;

namespace TestNinja.Mocking
{
    public interface IFileDownloader // inject into constructor of installler helper
    {
        void DownloadFile(string url, string path);
    }

    public class FileDownloader : IFileDownloader
    // encapsulating code that touches an external resource in FileDownloader
    {
        public void DownloadFile(string url, string path)
        {
            var client = new WebClient();

            client.DownloadFile(url, path);
        }
    }
}
