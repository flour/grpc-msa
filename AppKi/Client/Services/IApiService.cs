using AppKi.Shared;

namespace AppKi.Client.Services;

public interface IApiService
{
    IAsyncEnumerable<WeatherForecast> GetWeatherStream();
}