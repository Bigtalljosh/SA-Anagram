using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

/// <summary>
/// These tests don't run just yet, I had some odd behaviour with the async nature of the application
/// Unfortunately, Integration tests are not a mature feature for console applications in .NET at this time
/// In ASP.NET this is trivial to do with the use of WebApplicationFactory, you can spin up a version of your application
/// And feed it requests as normal asserting various results.
/// </summary>

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

            Assert.Equal("abc,bac,cba", output[1]);
            Assert.Equal("fun,unf", output[2]);
        }
    }
}