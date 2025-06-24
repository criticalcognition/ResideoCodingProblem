using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Services;

public interface IWeatherDataUrlService : IWeatherDataProcessor
{
    Task<List<CityWeatherData>> LoadWeatherDataFromUrlsAsync();
} 