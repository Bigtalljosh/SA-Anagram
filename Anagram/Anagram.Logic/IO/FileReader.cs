using Microsoft.Extensions.Logging;

namespace Anagram.Logic.IO;

public class FileReader : IFileReader
{
    private readonly ILogger _logger;

    public FileReader(ILogger<FileReader> logger)
    {
        _logger = logger;
    }

    public async IAsyncEnumerable<string> GetAllWordsOfLengthFromFile(string fileName, int length)
    {
        using StreamReader sr = new(fileName);
        string line;

        while ((line = await sr.ReadLineAsync()) != null && (line.Length <= length))
        {
            _logger.LogInformation($"Read word: {line}");
            if (line.Length.Equals(length))
                yield return line;
        }
    }
}
