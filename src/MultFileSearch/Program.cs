namespace MultFileSearch;
public class Program
{
    private static IConfigurationRoot Configuration { get; set; }
    private static ServiceCollection _serviceCollection { get; set; }
    private static ServiceProvider _serviceProvider { get; set; }

    private string folderPath = "/Users/Public/Downloads/ArchiveTrans/";
    async static Task Main(string[] args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");

        Configuration = LoadAppSettings();

        _serviceCollection = new ServiceCollection();
        RegisterAndInjectServices(_serviceCollection, Configuration);
        ConfigureDBContext(_serviceCollection);
        //Initialise netcore dependency injection provider
        _serviceProvider = _serviceCollection.BuildServiceProvider();

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

            // services.AddTransient<TestRegistrationContext>();
            // services.AddTransient<ScheduledUpdater>();
        }
}