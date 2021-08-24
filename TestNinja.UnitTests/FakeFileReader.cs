
using TestNinja.Mocking;


namespace TestNinja.UnitTests
{
    public class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return ""; // Not dependent on File, so we can use it in our tests
        }
    }
}
