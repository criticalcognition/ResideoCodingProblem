using ResideoCodingProblem.Models;
using ResideoCodingProblem.Services;

namespace ResideoCodingProblem.Tests.Services;

public class JsonOutputServiceTests
{
    [Fact]
    public void SerializeToJson_ShouldProduceValidJson()
    {
        // Arrange
        var service = new JsonOutputService();
        var data = new List<OutputRecord>
        {
            new("2019-03-28", 0.195, 5.97, "New York"),
            new("2019-03-29", 2.63, 17.42, "London")
        };

        // Act
        var json = service.SerializeToJson(data);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("\"date\": \"2019-03-28\"", json);
        Assert.Contains("\"city\": \"New York\"", json);
        Assert.Contains("\"min_temp\": 0.195", json);
        Assert.Contains("\"max_temp\": 5.97", json);
    }

    [Fact]
    public void SerializeToJson_ShouldHandleEmptyList()
    {
        // Arrange
        var service = new JsonOutputService();
        var data = new List<OutputRecord>();

        // Act
        var json = service.SerializeToJson(data);

        // Assert
        Assert.Equal("[]", json);
    }

    [Fact]
    public void SerializeToJson_ShouldProduceIndentedJson()
    {
        // Arrange
        var service = new JsonOutputService();
        var data = new List<OutputRecord>
        {
            new("2019-03-28", 0.195, 5.97, "New York")
        };

        // Act
        var json = service.SerializeToJson(data);

        // Assert
        Assert.Contains("\n", json); // Should be indented (contain newlines)
        Assert.Contains("  ", json); // Should contain spaces for indentation
    }

    [Fact]
    public void WriteToConsole_ShouldNotThrowException()
    {
        // Arrange
        var service = new JsonOutputService();
        var json = """[{"date":"2019-03-28","min_temp":0.195,"max_temp":5.97,"city":"New York"}]""";

        // Act & Assert
        var exception = Record.Exception(() => service.WriteToConsole(json));
        Assert.Null(exception);
    }
} 