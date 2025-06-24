using ResideoCodingProblem.Models;
using ResideoCodingProblem.Services;

namespace ResideoCodingProblem.Tests.Services;

public class WeatherDataProcessorBaseTests
{
    private class TestWeatherDataProcessor : WeatherDataProcessorBase
    {
        // Test implementation to access protected methods
    }

    [Fact]
    public void ProcessWeatherData_ShouldReturnCorrectNumberOfDays()
    {
        // Arrange
        var processor = new TestWeatherDataProcessor();
        var cityData = CreateTestCityData();

        // Act
        var result = processor.ProcessWeatherData(cityData);

        // Assert
        Assert.Equal(6, result.Count); // 6 days from 03/28/2019 to 04/02/2019
    }

    [Fact]
    public void ProcessWeatherData_ShouldReturnResultsInDateOrder()
    {
        // Arrange
        var processor = new TestWeatherDataProcessor();
        var cityData = CreateTestCityData();

        // Act
        var result = processor.ProcessWeatherData(cityData);

        // Assert
        var expectedDates = new[]
        {
            "2019-03-28", "2019-03-29", "2019-03-30",
            "2019-03-31", "2019-04-01", "2019-04-02"
        };

        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(expectedDates[i], result[i].Date);
        }
    }

    [Fact]
    public void ProcessWeatherData_ShouldFindCityWithLowestMinTemp()
    {
        // Arrange
        var processor = new TestWeatherDataProcessor();
        var cityData = new List<CityWeatherData>
        {
            new("New York", [
                new("2019-03-28", 0.195, 5.97),  // Lowest min temp
                new("2019-03-29", 3.655, 14.055)
            ]),
            new("Los Angeles", [
                new("2019-03-28", 9.445, 21.26),  // Higher min temp
                new("2019-03-29", 7.075, 22.19)
            ])
        };

        // Act
        var result = processor.ProcessWeatherData(cityData);

        // Assert
        var firstDay = result.First(r => r.Date == "2019-03-28");
        Assert.Equal("New York", firstDay.City);
        Assert.Equal(0.195, firstDay.MinTemp);
    }

    [Fact]
    public void ProcessWeatherData_ShouldHandleEmptyData()
    {
        // Arrange
        var processor = new TestWeatherDataProcessor();
        var cityData = new List<CityWeatherData>();

        // Act
        var result = processor.ProcessWeatherData(cityData);

        // Assert
        Assert.Empty(result);
    }

    private static List<CityWeatherData> CreateTestCityData()
    {
        return new List<CityWeatherData>
        {
            new("New York", [
                new("2019-03-28", 0.195, 5.97),
                new("2019-03-29", 3.655, 14.055),
                new("2019-03-30", 8.97, 17.115),
                new("2019-03-31", 6.29, 14.825),
                new("2019-04-01", 2.605, 8.39),
                new("2019-04-02", 3.65, 7.765)
            ]),
            new("Los Angeles", [
                new("2019-03-28", 9.445, 21.26),
                new("2019-03-29", 7.075, 22.19),
                new("2019-03-30", 6.545, 25.025),
                new("2019-03-31", 11.62, 27.755),
                new("2019-04-01", 13.165, 28.43),
                new("2019-04-02", 12.205, 24.105)
            ])
        };
    }
} 