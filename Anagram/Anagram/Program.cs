using Anagram;
using Anagram.Logic.IO;
using Anagram.Logic.Solvers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(builder =>
    {
        builder.AddConsole();
    })
    .ConfigureServices((_, services) =>
    {
        string inputFileName = args[0];
        Console.WriteLine($"Attempting to find file: {inputFileName}");
        Console.WriteLine($@"Full Path: {Directory.GetCurrentDirectory()}\Data\{inputFileName}");
        if (File.Exists($@"{Directory.GetCurrentDirectory()}\Data\{inputFileName}"))
        {
            services.AddOptions<ApplicationArguments>()
                    .Configure(x => x.DictionaryFileName = inputFileName)
                    .ValidateDataAnnotations();
        }
        else
        {
            Console.WriteLine("File not found");
            Console.WriteLine("Please make sure your file is in a folder called Data in the same directory as this executable.");
        }

        services.AddSingleton<IAnagramSolver, AnagramSolver>();
        services.AddSingleton<IFileReader, FileReader>();
    })
    .Build();

Start(host.Services);

await host.RunAsync();

static async void Start(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    FileReader reader = (FileReader)provider.GetService<IFileReader>();
    var wordList = reader.GetAllWordsOfLengthFromFile(@"Data\example2.txt", 8);

    AnagramSolver solver = (AnagramSolver)provider.GetService<IAnagramSolver>();
    var results = await solver.SolveAnagrams(wordList);

    var sb = new StringBuilder();

    foreach (var result in results)
    {
        sb.AppendLine($"Word {result.Key} has the anagrams: {string.Join(", ", result.Value)}");
    }

    Console.WriteLine(sb.ToString());
}