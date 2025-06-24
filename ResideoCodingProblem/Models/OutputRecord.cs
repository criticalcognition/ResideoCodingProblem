using System.Text.Json.Serialization;

namespace ResideoCodingProblem.Models;

public record OutputRecord(
    [property: JsonPropertyName("date")]
    string Date,
    
    [property: JsonPropertyName("min_temp")]
    double MinTemp,
    
    [property: JsonPropertyName("max_temp")]
    double MaxTemp,
    
    [property: JsonPropertyName("city")]
    string City
); 