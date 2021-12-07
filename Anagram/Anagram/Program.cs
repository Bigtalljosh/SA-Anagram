using Anagram;
using Anagram.Logic.IO;
using Anagram.Logic.Solvers;
using Anagram.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(builder =>
    {
        builder.AddConsole();
    })
    .ConfigureServices((_, services) =>
    {
        string inputFileName = args[0];

        if (!File.Exists(inputFileName))
        {
            Console.WriteLine("Cannot find the file specified. Please check the name and try again.");
            return;
        }
        else
        {
            services.AddOptions<ApplicationArguments>()
                    .Configure(x => x.DictionaryFileName = inputFileName)
                    .ValidateDataAnnotations();
        }

        services.AddSingleton<IRunner, Runner>();
        services.AddScoped<IAnagramSolver, AnagramSolver>();
        services.AddSingleton<IFileReader, FileReader>();
    })
    .Build();

Start(host.Services);

await host.RunAsync();

static async void Start(IServiceProvider services)
{
    Console.WriteLine("Starting Anagram Finder");

    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    var options = provider.GetService<IOptions<ApplicationArguments>>();

    Runner runner = (Runner)provider.GetService<IRunner>();
    await runner.Run($@"{options.Value.DictionaryFileName}");
    Environment.Exit(0);
}