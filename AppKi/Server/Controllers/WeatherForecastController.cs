using System.Diagnostics;
using ApiOne.Client;
using ApiOne.Client.Contracts.Requests;
using AppKi.Shared;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;

namespace AppKi.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IApiOneService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            IApiOneService service,
            ILogger<WeatherForecastController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            _logger.LogInformation("Get Weather");
            using var source = new ActivitySource("TestSource");
            using var activity = source.CreateActivity("TestActivity", ActivityKind.Server);
            activity?.AddBaggage("test1", "one");
            
            
            var result = await _service.OneCall(new OneRequest {Query = "test"}, HttpContext.RequestAborted);
            _logger.LogInformation("Got Weather response");
            return new List<WeatherForecast>
            {
                new()
                {
                    Date = result.Date,
                    Summary = result.Summary,
                    TemperatureC = (int) result.TemperatureC,
                }
            };
        }
    }
}