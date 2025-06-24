using ResideoCodingProblem.Services;
using ResideoCodingProblem.Models;

namespace ResideoCodingProblem;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // Parse command line arguments
            var useUrls = ParseCommandLineArgs(args);

            // Initialize services
            IWeatherDataService weatherService = new WeatherDataService();
            IWeatherDataUrlService weatherUrlService = new WeatherDataUrlService();
            IJsonOutputService jsonOutputService = new JsonOutputService();

            List<CityWeatherData> allCityData;

            if (useUrls)
            {
                Console.WriteLine("Loading weather data from URLs...");
                allCityData = await weatherUrlService.LoadWeatherDataFromUrlsAsync();
            }
            else
            {
                Console.WriteLine("Loading weather data from local files...");
                allCityData = await weatherService.LoadWeatherDataAsync();
            }

            if (allCityData.Count == 0)
            {
                Console.WriteLine("No weather data loaded. Exiting.");
                return;
            }

            // Process the data to find the city with lowest minimum temperature for each day
            var result = useUrls 
                ? weatherUrlService.ProcessWeatherData(allCityData)
                : weatherService.ProcessWeatherData(allCityData);

            // Output the result as JSON
            var outputJson = jsonOutputService.SerializeToJson(result);
            jsonOutputService.WriteToConsole(outputJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static bool ParseCommandLineArgs(string[] args)
    {
        if (args.Length == 0)
        {
            return false; // Default to local files
        }

        var firstArg = args[0].ToLowerInvariant();
        
        if (firstArg == "--url" || firstArg == "-u" || firstArg == "--urls")
        {
            return true;
        }
        
        if (firstArg == "--help" || firstArg == "-h" || firstArg == "-?")
        {
            ShowHelp();
            Environment.Exit(0);
        }

        Console.WriteLine($"Unknown argument: {firstArg}");
        ShowHelp();
        Environment.Exit(1);
        
        return false; // This line won't be reached, but needed for compilation
    }

    private static void ShowHelp()
    {
        Console.WriteLine("Resideo Weather Data Processor");
        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine("  dotnet run                    # Load data from local JsonFiles directory (default)");
        Console.WriteLine("  dotnet run --url             # Load data from S3 URLs");
        Console.WriteLine("  dotnet run -u                # Short form for --url");
        Console.WriteLine("  dotnet run --help            # Show this help message");
        Console.WriteLine();
        Console.WriteLine("The program will output JSON containing the city with the lowest");
        Console.WriteLine("minimum temperature for each day from 03/28/2019 to 04/02/2019.");
    }
}