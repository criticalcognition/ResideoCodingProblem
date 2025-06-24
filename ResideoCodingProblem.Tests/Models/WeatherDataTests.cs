using System.Text.Json;
using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Tests.Models;

public class WeatherDataTests
{
    [Fact]
    public void WeatherData_ShouldSerializeToCorrectJson()
    {
        // Arrange
        var weatherData = new WeatherData(
            ApplicableDate: "2019-03-28",
            MinTemp: 0.195,
            MaxTemp: 5.97
        );

        // Act
        var json = JsonSerializer.Serialize(weatherData);

        // Assert
        var expectedJson = """{"applicable_date":"2019-03-28","min_temp":0.195,"max_temp":5.97}""";
        Assert.Equal(expectedJson, json);
    }

    [Fact]
    public void WeatherData_ShouldDeserializeFromJson()
    {
        // Arrange
        var json = """{"applicable_date":"2019-03-28","min_temp":0.195,"max_temp":5.97}""";

        // Act
        var weatherData = JsonSerializer.Deserialize<WeatherData>(json);

        // Assert
        Assert.NotNull(weatherData);
        Assert.Equal("2019-03-28", weatherData.ApplicableDate);
        Assert.Equal(0.195, weatherData.MinTemp);
        Assert.Equal(5.97, weatherData.MaxTemp);
    }

    [Fact]
    public void WeatherData_ShouldSupportRecordEquality()
    {
        // Arrange
        var weatherData1 = new WeatherData("2019-03-28", 0.195, 5.97);
        var weatherData2 = new WeatherData("2019-03-28", 0.195, 5.97);
        var weatherData3 = new WeatherData("2019-03-29", 0.195, 5.97);

        // Act & Assert
        Assert.Equal(weatherData1, weatherData2);
        Assert.NotEqual(weatherData1, weatherData3);
    }
} 