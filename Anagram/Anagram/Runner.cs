using Anagram.Logic.IO;
using Anagram.Logic.Solvers;
using System.Text;

namespace Anagram.Runner
{
    public class Runner : IRunner
    {
        private readonly IFileReader _fileReader;
        private readonly IAnagramSolver _anagramSolver;

        public Runner(IFileReader fileReader, IAnagramSolver anagramSolver)
        {
            _fileReader = fileReader;
            _anagramSolver = anagramSolver;
        }

        public async Task Run(string fileName)
        {
            for (int i = 28; i >= 1; i--)
            {
                var sb = new StringBuilder();
                var wordList = _fileReader.GetAllWordsOfLengthFromFile(fileName, i);
                var anagramList = await _anagramSolver.SolveAnagrams(wordList);

                foreach (var anagram in anagramList)
                    sb.AppendLine($"Word {anagram.Key} has the anagrams: {string.Join(", ", anagram.Value)}");

                Console.WriteLine(sb.ToString());

#if DEBUG
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), $"output-{i}-length-words.txt");
                File.WriteAllText(fullPath, sb.ToString(), Encoding.UTF8);
#endif
                sb.Clear();
            }
        }
    }
}
