
namespace Anagram.Logic.IO
{
    public interface IFileReader
    {
        IAsyncEnumerable<string> GetAllWordsOfLengthFromFile(string fileName, int length);
    }
}