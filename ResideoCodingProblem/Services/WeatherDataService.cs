using System.Text.Json;
using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Services;

public class WeatherDataService(string jsonFilesDirectory = "JsonFiles") : WeatherDataProcessorBase, IWeatherDataService
{
    public async Task<List<CityWeatherData>> LoadWeatherDataAsync()
    {
        var allCityData = new List<CityWeatherData>();

        if (!Directory.Exists(jsonFilesDirectory))
        {
            Console.WriteLine($"Warning: Directory {jsonFilesDirectory} not found.");
            return allCityData;
        }

        var jsonFiles = Directory.GetFiles(jsonFilesDirectory, "*.json");

        foreach (var filePath in jsonFiles)
        {
            try
            {
                var jsonContent = await File.ReadAllTextAsync(filePath);
                var cityData = JsonSerializer.Deserialize<CityWeatherData>(jsonContent);
                if (cityData != null)
                {
                    allCityData.Add(cityData);
                    Console.WriteLine($"Loaded weather data for {cityData.Title} from {Path.GetFileName(filePath)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        return allCityData;
    }
} 