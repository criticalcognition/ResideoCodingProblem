using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Services;

public interface IWeatherDataProcessor
{
    List<OutputRecord> ProcessWeatherData(List<CityWeatherData> allCityData);
} 