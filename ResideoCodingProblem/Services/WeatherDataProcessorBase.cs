using ResideoCodingProblem.Models;

namespace ResideoCodingProblem.Services;

public abstract class WeatherDataProcessorBase
{
    public List<OutputRecord> ProcessWeatherData(List<CityWeatherData> allCityData)
    {
        var result = new List<OutputRecord>();
        
        // Define the date range (03/28/2019 - 04/02/2019)
        var startDate = new DateTime(2019, 3, 28);
        var endDate = new DateTime(2019, 4, 2);
        
        // Process each date
        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            var dateString = date.ToString("yyyy-MM-dd");
            var lowestTempCity = FindCityWithLowestMinTemp(allCityData, dateString);
            
            if (lowestTempCity != null)
            {
                result.Add(lowestTempCity);
            }
        }
        
        return result;
    }

    protected static OutputRecord? FindCityWithLowestMinTemp(List<CityWeatherData> allCityData, string dateString)
    {
        OutputRecord? lowestTempRecord = null;
        double lowestMinTemp = double.MaxValue;

        foreach (var cityData in allCityData)
        {
            var weatherForDate = cityData.ConsolidatedWeather
                .FirstOrDefault(w => w.ApplicableDate == dateString);

            if (weatherForDate != null && weatherForDate.MinTemp < lowestMinTemp)
            {
                lowestMinTemp = weatherForDate.MinTemp;
                lowestTempRecord = new OutputRecord(
                    Date: weatherForDate.ApplicableDate,
                    MinTemp: weatherForDate.MinTemp,
                    MaxTemp: weatherForDate.MaxTemp,
                    City: cityData.Title
                );
            }
        }

        return lowestTempRecord;
    }
} 