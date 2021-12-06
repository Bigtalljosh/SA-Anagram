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
        await foreach (var word in words)
        {
            if (_anagrams.ContainsKey(word))
            {
                _logger.LogInformation($"Word '{word}' was already in Dictionary, Adding to list.");
                _anagrams[word].Add(word); // Just add the dupe for now
            }
            else
            {
                _logger.LogInformation($"Word '{word}' was not in Dictionary, Checking if an anagram exists.");
                var (result, key) = IsAnagramInDictionary(word); // Check if an anagram is already in dictionary
                if (result)
                {
                    _logger.LogInformation($"Word '{word}' already has an anagram for this word in Dictionary, Adding to list.");
                    _anagrams[key].Add(word); // If there is an anagram, add it to it's list
                }
                else
                {
                    _logger.LogInformation($"Word '{word}' is a completely new word, adding to dictionary.");
                    _anagrams.Add(word, new List<string>()); // Otherwise add the new word
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
