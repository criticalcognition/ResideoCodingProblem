using System.Text.Json;
using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Tests.Models;

public class OutputRecordTests
{
    [Fact]
    public void OutputRecord_ShouldSerializeToCorrectJson()
    {
        // Arrange
        var outputRecord = new OutputRecord(
            Date: "2019-03-28",
            MinTemp: 0.195,
            MaxTemp: 5.97,
            City: "New York"
        );

        // Act
        var json = JsonSerializer.Serialize(outputRecord);

        // Assert
        var expectedJson = """{"date":"2019-03-28","min_temp":0.195,"max_temp":5.97,"city":"New York"}""";
        Assert.Equal(expectedJson, json);
    }

    [Fact]
    public void OutputRecord_ShouldDeserializeFromJson()
    {
        // Arrange
        var json = """{"date":"2019-03-28","min_temp":0.195,"max_temp":5.97,"city":"New York"}""";

        // Act
        var outputRecord = JsonSerializer.Deserialize<OutputRecord>(json);

        // Assert
        Assert.NotNull(outputRecord);
        Assert.Equal("2019-03-28", outputRecord.Date);
        Assert.Equal(0.195, outputRecord.MinTemp);
        Assert.Equal(5.97, outputRecord.MaxTemp);
        Assert.Equal("New York", outputRecord.City);
    }

    [Fact]
    public void OutputRecord_ShouldSupportRecordEquality()
    {
        // Arrange
        var record1 = new OutputRecord("2019-03-28", 0.195, 5.97, "New York");
        var record2 = new OutputRecord("2019-03-28", 0.195, 5.97, "New York");
        var record3 = new OutputRecord("2019-03-28", 0.195, 5.97, "Los Angeles");

        // Act & Assert
        Assert.Equal(record1, record2);
        Assert.NotEqual(record1, record3);
    }
} 