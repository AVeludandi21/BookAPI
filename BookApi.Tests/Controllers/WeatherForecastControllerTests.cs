using Xunit;
using BookApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookApi.Tests.Controllers
{
    public class WeatherForecastControllerTests
    {
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            // Initialize the controller with a mock logger
            var mockLogger = new Mock<ILogger<WeatherForecastController>>().Object;
            _controller = new WeatherForecastController(mockLogger);
        }

        [Fact]
        public void Get_ReturnsFiveWeatherForecasts()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void Get_ReturnsValidWeatherForecasts()
        {
            // Act
            var result = _controller.Get();

            // Assert
            foreach (var forecast in result)
            {
                Assert.NotNull(forecast);
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.NotNull(forecast.Summary);
                Assert.Contains(forecast.Summary, new[] 
                { 
                    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" 
                });
            }
        }

        [Fact]
        public void Get_ReturnsCorrectDates()
        {
            // Act
            var result = _controller.Get();

            // Assert
            var expectedDates = Enumerable.Range(1, 5).Select(i => DateOnly.FromDateTime(DateTime.Now.AddDays(i))).ToArray();
            var actualDates = result.Select(f => f.Date).ToArray();

            Assert.Equal(expectedDates, actualDates);
        }
    }
}