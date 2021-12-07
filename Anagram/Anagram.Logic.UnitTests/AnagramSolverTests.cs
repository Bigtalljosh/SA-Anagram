using Anagram.Logic.Solvers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Anagram.Logic.UnitTests
{
    public class AnagramSolverTests
    {
        readonly Mock<ILogger<AnagramSolver>> _loggerMock;

        public AnagramSolverTests()
        {
            _loggerMock = new Mock<ILogger<AnagramSolver>>();
        }

        [Theory]
        [InlineData("abc", 3)]
        [InlineData("dar", 1)]
        [InlineData("dog", 2)]
        public async void SolveAnagrams_ShouldReturnAllAnagramsInWordList(string key, int expectedResults)
        {
            var wordList = GetTestValues();
            var sut = new AnagramSolver(_loggerMock.Object);

            var results = await sut.SolveAnagrams(wordList);

            Assert.Equal(expectedResults, results[key].Count);
        }

        [Fact]
        public async void SolveAnagrams_ShouldReturnEmptyDictionary_WhenPassedEmptyIAsyncEnumberable()
        {
            var wordList = GetEmptyTestValues();
            var sut = new AnagramSolver(_loggerMock.Object);

            var results = await sut.SolveAnagrams(wordList);

            Assert.NotNull(results);
            Assert.Empty(results);
        }

        [Theory]
        [InlineData("abc", 3)]
        [InlineData("dar", 1)]
        [InlineData("dog", 2)]
        public async void SolveAnagrams_OnlyReturnOneSetOfAnagrams_WhenFunctionCalledTwice(string key, int expectedResults)
        {
            var wordList = GetTestValues();
            var sut = new AnagramSolver(_loggerMock.Object);

            await sut.SolveAnagrams(wordList);
            var results = await sut.SolveAnagrams(wordList);

            Assert.Equal(expectedResults, results[key].Count);
        }

        private static async IAsyncEnumerable<string> GetTestValues()
        {
            yield return "abc";
            yield return "cba";
            yield return "cab";
            yield return "dar";
            yield return "dog";
            yield return "god";

            await Task.CompletedTask;
        }

        private static async IAsyncEnumerable<string> GetEmptyTestValues()
        {
            yield break;
        }
    }
}
