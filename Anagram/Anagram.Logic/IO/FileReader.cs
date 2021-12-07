namespace Anagram.Logic.IO;

public class FileReader : IFileReader
{
    public async IAsyncEnumerable<string> GetAllWordsOfLengthFromFile(string fileName, int length)
    {
        using StreamReader sr = new(fileName);
        string line;

        while ((line = await sr.ReadLineAsync()) != null && (line.Length <= length))
        {
            if (line.Length.Equals(length))
                yield return line;
        }
    }
}