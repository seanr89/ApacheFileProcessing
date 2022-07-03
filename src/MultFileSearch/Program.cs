using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MultFileSearch;
public class Program
{
    private static IConfigurationRoot Configuration { get; set; }
    private static ServiceCollection _serviceCollection { get; set; }
    private static ServiceProvider _serviceProvider { get; set; }

    private static string windowsFolderPath = "/Users/Public/Downloads/ArchiveTrans/";
    private static string macFolderPath = "/Users/seanrafferty/Documents/AppFiles/";

    async static Task Main(string[] args)
    {
        Console.WriteLine("File Searcher Started!");

        bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);
        string folderPath = isWindows ? windowsFolderPath : macFolderPath;


        Configuration = LoadAppSettings();

        _serviceCollection = new ServiceCollection();
        RegisterAndInjectServices(_serviceCollection, Configuration);
        //Initialise netcore dependency injection provider
        _serviceProvider = _serviceCollection.BuildServiceProvider();

        Console.WriteLine("Please enter a customer ID");
        string customerID = Console.ReadLine() ?? "27fe7bf7-635f-4067-a68d-dfbca9006ffe";
        Console.WriteLine("Please end a MID");
        string mid = Console.ReadLine() ?? "123";

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try{
            _serviceProvider.GetService<FolderProcessor>()?.Execute(folderPath, isWindows, customerID);//.Wait();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        stopwatch.Stop();
        Console.WriteLine($"Event completed it : {stopwatch.ElapsedMilliseconds}ms");

        Console.ReadLine();
        Console.WriteLine("File Search Completed!");
    }

    /// <summary>
    /// Query app settings json content
    /// </summary>
    /// <returns></returns>
    private static IConfigurationRoot LoadAppSettings()
    {
        Console.WriteLine("LoadAppSettings");
        try
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

            return config;
        }
        catch (System.IO.FileNotFoundException)
        {
            Console.WriteLine("Error trying to load app settings");
            return null;
        }
    }

    /// <summary>
    /// Prep/Configure Dependency Injection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    private static void RegisterAndInjectServices(IServiceCollection services, IConfiguration config)
    {
        Console.WriteLine("RegisterAndInjectServices");
        services.AddLogging(logging =>
        {
            logging.AddConsole();
        }).Configure<LoggerFilterOptions>(options => options.MinLevel =
                                            LogLevel.Warning);

        services.AddTransient<FolderProcessor>();
        // services.AddTransient<ScheduledUpdater>();
    }
}