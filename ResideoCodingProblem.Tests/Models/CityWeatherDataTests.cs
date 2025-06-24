using System.Text.Json;
using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Tests.Models;

public class CityWeatherDataTests
{
    [Fact]
    public void CityWeatherData_ShouldSerializeToCorrectJson()
    {
        // Arrange
        var weatherData = new WeatherData("2019-03-28", 0.195, 5.97);
        var cityWeatherData = new CityWeatherData(
            Title: "New York",
            ConsolidatedWeather: [weatherData]
        );

        // Act
        var json = JsonSerializer.Serialize(cityWeatherData);

        // Assert
        Assert.Contains("\"title\":\"New York\"", json);
        Assert.Contains("\"consolidated_weather\":", json);
        Assert.Contains("\"applicable_date\":\"2019-03-28\"", json);
    }

    [Fact]
    public void CityWeatherData_ShouldDeserializeFromJson()
    {
        // Arrange
        var json = """{"title":"New York","consolidated_weather":[{"applicable_date":"2019-03-28","min_temp":0.195,"max_temp":5.97}]}""";

        // Act
        var cityWeatherData = JsonSerializer.Deserialize<CityWeatherData>(json);

        // Assert
        Assert.NotNull(cityWeatherData);
        Assert.Equal("New York", cityWeatherData.Title);
        Assert.Single(cityWeatherData.ConsolidatedWeather);
        Assert.Equal("2019-03-28", cityWeatherData.ConsolidatedWeather[0].ApplicableDate);
    }

    [Fact]
    public void CityWeatherData_ShouldSupportRecordEquality()
    {
        // Arrange
        var weatherData = new WeatherData("2019-03-28", 0.195, 5.97);
        var weatherArray = new[] { weatherData };
        var cityData1 = new CityWeatherData("New York", weatherArray);
        var cityData2 = new CityWeatherData("New York", weatherArray);
        var cityData3 = new CityWeatherData("Los Angeles", weatherArray);

        // Act & Assert
        Assert.Equal(cityData1, cityData2);
        Assert.NotEqual(cityData1, cityData3);
    }
} 