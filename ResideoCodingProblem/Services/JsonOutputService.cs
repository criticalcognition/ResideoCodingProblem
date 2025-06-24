using System.Text.Json;
using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Services;

public class JsonOutputService : IJsonOutputService
{
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonOutputService()
    {
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
    }

    public string SerializeToJson(List<OutputRecord> data)
    {
        return JsonSerializer.Serialize(data, _jsonOptions);
    }

    public void WriteToConsole(string json)
    {
        Console.WriteLine(json);
    }
} 