using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Services;

public interface IWeatherDataService : IWeatherDataProcessor
{
    Task<List<CityWeatherData>> LoadWeatherDataAsync();
} 