using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Anagram.Runner.IntegrationTests
{
    public class RunnerTests
    {
        private const string _testFile = @"Data\example1.txt";

        private static Process StartApplication(string args = "")
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = $@"..\..\..\..\Anagram\bin\Debug\net6.0\Anagram.Runner.exe",
                Arguments = args,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };

            return Process.Start(processStartInfo);
        }

        [Fact]
        public async void Run_ShouldRunApplication()
        {
            var process = StartApplication(_testFile);

            var output = await process.StandardOutput.ReadLineAsync();
            await process.WaitForExitAsync();

            Assert.Contains("Starting Anagram Finder", output);
        }

        [Fact]
        public async void Run_ShouldProduceExpectedOutputForExample1()
        {
            var process = StartApplication(_testFile);
            var output = new List<string>();

            while (!process.StandardOutput.EndOfStream)
            {
                output.Add(await process.StandardOutput.ReadLineAsync());
            }

            await process.WaitForExitAsync();

            Assert.Contains("Word abc has the anagrams: abc, bac, cba", output);
            Assert.Contains("Word fun has the anagrams: fun, fun, unf", output);
        }
    }
}