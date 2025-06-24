using System.Text.Json;
using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Services;

public class WeatherDataUrlService : WeatherDataProcessorBase, IWeatherDataUrlService
{
    private readonly string[] _urls;
    private readonly HttpClient _httpClient;

    public WeatherDataUrlService()
    {
        _urls = 
        [
            "https://s3.amazonaws.com/connectedsavings.com/coding/weather/401024.json", // New York
            "https://s3.amazonaws.com/connectedsavings.com/coding/weather/746560.json", // Los Angeles
            "https://s3.amazonaws.com/connectedsavings.com/coding/weather/77536.json"   // London
        ];
        
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<List<CityWeatherData>> LoadWeatherDataFromUrlsAsync()
    {
        var allCityData = new List<CityWeatherData>();

        foreach (var url in _urls)
        {
            try
            {
                var jsonContent = await _httpClient.GetStringAsync(url);
                var cityData = JsonSerializer.Deserialize<CityWeatherData>(jsonContent);
                if (cityData != null)
                {
                    allCityData.Add(cityData);
                    Console.WriteLine($"Loaded weather data for {cityData.Title} from {url}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from URL {url}: {ex.Message}");
            }
        }

        return allCityData;
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
} 