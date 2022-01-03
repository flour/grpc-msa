using AppKi.Shared;

namespace AppKi.Client.Services;

internal class ApiService : IApiService
{
    private readonly HttpClient _client;
    private readonly ILogger<ApiService> _logger;

    public ApiService(
        HttpClient client,
        ILogger<ApiService> logger)
    {
        _client = client;
        _logger = logger;
    }


    public IAsyncEnumerable<WeatherForecast> GetWeatherStream()
    {
        throw new NotImplementedException();
    }
}