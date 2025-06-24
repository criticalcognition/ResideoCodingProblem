using System.Text.Json.Serialization;

namespace ResideoCodingProblem.Models;

public record CityWeatherData(
    [property: JsonPropertyName("title")]
    string Title,
    
    [property: JsonPropertyName("consolidated_weather")]
    WeatherData[] ConsolidatedWeather
); 