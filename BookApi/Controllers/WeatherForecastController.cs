using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    // This is a controller for handling API requests related to weather forecasts.
    [ApiController] // Marks this class as a Web API controller.
    [Route("[controller]")] // Sets the route for this controller to match its name (e.g., "WeatherForecast").
    public class WeatherForecastController : ControllerBase
    {
        // A list of possible weather descriptions
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // A logger used to log information, warnings, or errors (optional for this example).
        private readonly ILogger<WeatherForecastController> _logger;

        // Constructor to set up the logger (provided automatically by ASP.NET).
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // This method handles GET requests to fetch weather forecasts.
        [HttpGet(Name = "GetWeatherForecast")] // Specifies this method is for HTTP GET requests.
        public IEnumerable<WeatherForecast> Get()
        {
            // Creates 5 random weather forecasts.
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                // Sets the date to today plus a few days (e.g., 1 to 5 days in the future).
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                
                // Generates a random temperature in Celsius between -20 and 55.
                TemperatureC = Random.Shared.Next(-20, 55),
                
                // Picks a random weather description from the Summaries list.
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray(); // Converts the result into an array.
        }
    }
}
