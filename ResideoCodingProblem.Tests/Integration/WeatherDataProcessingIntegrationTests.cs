using ResideoCodingProblem.Models;
using ResideoCodingProblem.Services;

namespace ResideoCodingProblem.Tests.Integration;

public class WeatherDataProcessingIntegrationTests
{
    [Fact]
    public async Task CompleteWorkflow_ShouldProcessWeatherDataCorrectly()
    {
        // Arrange
        var weatherService = new WeatherDataService("TestData");
        var jsonOutputService = new JsonOutputService();

        // Create test data files
        await CreateTestDataFiles();

        try
        {
            // Act
            var allCityData = await weatherService.LoadWeatherDataAsync();
            var result = weatherService.ProcessWeatherData(allCityData);
            var outputJson = jsonOutputService.SerializeToJson(result);

            // Assert
            Assert.NotEmpty(allCityData);
            Assert.Equal(2, result.Count); // 2 days from test data
            Assert.NotNull(outputJson);
            Assert.Contains("\"date\": \"2019-03-28\"", outputJson);
        }
        finally
        {
            // Cleanup
            CleanupTestDataFiles();
        }
    }

    [Fact]
    public void ProcessingLogic_ShouldFindCorrectLowestTemperatures()
    {
        // Arrange
        var processor = new WeatherDataService();
        var testData = new List<CityWeatherData>
        {
            new("New York", [
                new("2019-03-28", 0.195, 5.97),   // Lowest for 03-28
                new("2019-03-29", 3.655, 14.055)  // Not lowest for 03-29
            ]),
            new("London", [
                new("2019-03-28", 3.74, 16.24),   // Higher for 03-28
                new("2019-03-29", 2.63, 17.42)    // Lowest for 03-29
            ])
        };

        // Act
        var result = processor.ProcessWeatherData(testData);

        // Assert
        var day1 = result.First(r => r.Date == "2019-03-28");
        var day2 = result.First(r => r.Date == "2019-03-29");

        Assert.Equal("New York", day1.City);
        Assert.Equal(0.195, day1.MinTemp);

        Assert.Equal("London", day2.City);
        Assert.Equal(2.63, day2.MinTemp);
    }

    private static async Task CreateTestDataFiles()
    {
        var testDataDir = "TestData";
        Directory.CreateDirectory(testDataDir);

        var newYorkData = """{"title":"New York","consolidated_weather":[{"applicable_date":"2019-03-28","min_temp":0.195,"max_temp":5.97},{"applicable_date":"2019-03-29","min_temp":3.655,"max_temp":14.055}]}""";
        var losAngelesData = """{"title":"Los Angeles","consolidated_weather":[{"applicable_date":"2019-03-28","min_temp":9.445,"max_temp":21.26},{"applicable_date":"2019-03-29","min_temp":7.075,"max_temp":22.19}]}""";

        await File.WriteAllTextAsync(Path.Combine(testDataDir, "test1.json"), newYorkData);
        await File.WriteAllTextAsync(Path.Combine(testDataDir, "test2.json"), losAngelesData);
    }

    private static void CleanupTestDataFiles()
    {
        var testDataDir = "TestData";
        if (Directory.Exists(testDataDir))
        {
            Directory.Delete(testDataDir, true);
        }
    }
} 