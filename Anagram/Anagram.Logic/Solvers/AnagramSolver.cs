using Microsoft.Extensions.Logging;

namespace Anagram.Logic.Solvers;

public class AnagramSolver : IAnagramSolver
{
    private readonly ILogger<AnagramSolver> _logger;

    private readonly Dictionary<string, List<string>> _anagrams;

    public AnagramSolver(ILogger<AnagramSolver> logger)
    {
        _logger = logger;
        _anagrams = new Dictionary<string, List<string>>();
    }

    public async Task<Dictionary<string, List<string>>> SolveAnagrams(IAsyncEnumerable<string> words)
    {
        _anagrams.Clear();

        await foreach (var word in words)
        {
            if (_anagrams.ContainsKey(word))
            {
                _logger.LogDebug($"Word: {word} was found in the dictionary. Adding dupe to anagram list.");
                _anagrams[word].Add(word);
            }
            else
            {
                var (result, key) = IsAnagramInDictionary(word);
                if (result)
                {
                    _logger.LogDebug($"Word: {word} already has an anagram found in the dictionary. Adding word to anagram list.");
                    _anagrams[key].Add(word);
                }
                else
                {
                    _logger.LogDebug($"Word: {word} is a new word and will be added to the dictionary.");
                    _anagrams.Add(word, new List<string> { word });
                }
            }
        }

        return _anagrams;
    }

    private (bool result, string? key) IsAnagramInDictionary(string word)
    {
        foreach (var key in _anagrams.Keys)
        {
            if (IsAnagram(key, word))
                return (true, key);
        }

        return (false, null);
    }

    private static bool IsAnagram(string word1, string word2)
    {
        foreach (var letter in word1.ToLowerInvariant())
        {
            if (!word2.ToLowerInvariant().Contains(letter))
                return false;
        }

        return true;
    }
}
