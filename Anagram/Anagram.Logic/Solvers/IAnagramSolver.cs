namespace Anagram.Logic.Solvers;

public interface IAnagramSolver
{
    Task<Dictionary<string, List<string>>> SolveAnagrams(IAsyncEnumerable<string> words);
}
