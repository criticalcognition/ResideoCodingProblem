using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Services;

public interface IJsonOutputService
{
    string SerializeToJson(List<OutputRecord> data);
    void WriteToConsole(string json);
} 