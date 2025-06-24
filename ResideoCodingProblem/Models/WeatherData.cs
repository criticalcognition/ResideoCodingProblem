using System.Text.Json.Serialization;

namespace ResideoCodingProblem.Models;

public record WeatherData(
    [property: JsonPropertyName("applicable_date")]
    string ApplicableDate,
    
    [property: JsonPropertyName("min_temp")]
    double MinTemp,
    
    [property: JsonPropertyName("max_temp")]
    double MaxTemp
); 