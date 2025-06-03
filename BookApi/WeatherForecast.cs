// WeatherForecast.cs
// This file defines the WeatherForecast model used by the API to represent weather forecast data.

namespace BookApi
{
    // Represents a weather forecast for a specific date, including temperature and summary.
    public class WeatherForecast
    {
        // The date of the weather forecast.
        public DateOnly Date { get; set; }

        // The temperature in degrees Celsius.
        public int TemperatureC { get; set; }

        // The temperature in degrees Fahrenheit (calculated from Celsius).
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        // A short summary or description of the weather.
        public string? Summary { get; set; }
    }
}
