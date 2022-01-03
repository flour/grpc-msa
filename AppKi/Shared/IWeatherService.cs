using System.ServiceModel;

namespace AppKi.Shared;

[ServiceContract]
public interface IWeatherService
{
    [OperationContract]
    IAsyncEnumerable<WeatherForecast> GetWeatherStream(CancellationToken token = default);
}