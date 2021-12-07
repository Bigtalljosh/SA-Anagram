using Anagram.Logic.IO;
using System.Threading.Tasks;
using Xunit;

namespace Anagram.Logic.UnitTests
{
    public class FileReaderTests
    {
        private const string TestDataFilename = "testdata.txt";

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(19)]
        public async Task GetAllWordsOfLengthFromFile_ShouldReturnOnlyWordsOfTheSameLength(int wordLength)
        {
            var sut = new FileReader();

            var results = sut.GetAllWordsOfLengthFromFile(TestDataFilename, wordLength);

            await foreach (var word in results)
            {
                Assert.Equal(wordLength, word.Length);
            }
        }
    }
}