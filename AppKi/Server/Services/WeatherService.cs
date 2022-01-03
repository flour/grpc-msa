using System.Runtime.CompilerServices;
using ApiOne.Client;
using ApiOne.Client.Contracts.Requests;
using AppKi.Shared;
using Mapster;

namespace AppKi.Server.Services;

internal class WeatherService : IWeatherService
{
    private readonly IApiOneService _apiOneService;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(IApiOneService apiOneService, ILogger<WeatherService> logger)
    {
        _apiOneService = apiOneService;
        _logger = logger;
    }
    
    public async IAsyncEnumerable<WeatherForecast> GetWeatherStream([EnumeratorCancellation] CancellationToken token = default)
    {
        await foreach (var item in _apiOneService.StreamCall(new OneRequest {Query = "test"}, token))
        {
            yield return item.Adapt<WeatherForecast>();
        }
    }
}