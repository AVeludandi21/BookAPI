// Unit tests for WeatherForecastController.
// These tests verify the controller's behavior for generating weather forecasts.

using Xunit;
using BookApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookApi.Tests.Controllers
{
    /// <summary>
    /// Contains unit tests for the WeatherForecastController class.
    /// </summary>
    public class WeatherForecastControllerTests
    {
        // Instance of the controller under test.
        private readonly WeatherForecastController _controller;

        /// <summary>
        /// Initializes a new instance of WeatherForecastController for each test, using a mock logger.
        /// </summary>
        public WeatherForecastControllerTests()
        {
            // Initialize the controller with a mock logger
            var mockLogger = new Mock<ILogger<WeatherForecastController>>().Object;
            _controller = new WeatherForecastController(mockLogger);
        }

        /// <summary>
        /// Tests that Get returns exactly five weather forecasts.
        /// </summary>
        [Fact]
        public void Get_ReturnsFiveWeatherForecasts()
        {
            // Act: Call the method to get weather forecasts
            var result = _controller.Get();

            // Assert: The result should not be null and should contain exactly 5 items
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        /// <summary>
        /// Tests that Get returns valid weather forecast data for each forecast.
        /// </summary>
        [Fact]
        public void Get_ReturnsValidWeatherForecasts()
        {
            // Act: Call the method to get weather forecasts
            var result = _controller.Get();

            // Assert: Each forecast should have valid data
            foreach (var forecast in result)
            {
                Assert.NotNull(forecast); // Forecast object should not be null
                Assert.InRange(forecast.TemperatureC, -20, 55); // Temperature should be within expected range
                Assert.NotNull(forecast.Summary); // Summary should not be null
                Assert.Contains(forecast.Summary, new[] 
                { 
                    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" 
                }); // Summary should be one of the expected values
            }
        }

        /// <summary>
        /// Tests that Get returns forecasts with the correct dates (next 5 days).
        /// </summary>
        [Fact]
        public void Get_ReturnsCorrectDates()
        {
            // Act: Call the method to get weather forecasts
            var result = _controller.Get();

            // Assert: The dates should be the next 5 days from today
            var expectedDates = Enumerable.Range(1, 5).Select(i => DateOnly.FromDateTime(DateTime.Now.AddDays(i))).ToArray();
            var actualDates = result.Select(f => f.Date).ToArray();

            Assert.Equal(expectedDates, actualDates);
        }
    }
}