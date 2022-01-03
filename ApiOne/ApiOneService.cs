using System.Diagnostics;
using ApiOne.Client;
using ApiOne.Client.Contracts.Requests;
using ApiOne.Client.Contracts.Responses;
using ApiTwo.Client;

namespace ApiOne
{
    internal class ApiOneService : IApiOneService
    {
        private readonly IApiTwoService _apiTwo;
        private readonly ILogger<ApiOneService> _logger;

        public ApiOneService(IApiTwoService apiTwo, ILogger<ApiOneService> logger)
        {
            _apiTwo = apiTwo;
            _logger = logger;
        }

        public async ValueTask<OneResponse> OneCall(OneRequest request, CancellationToken token = default)
        {
            using var activity = Activity.Current?.Source.CreateActivity("Tha one", ActivityKind.Server);

            activity?.AddTag("api.two.custom.tag", "tag");

            _logger.LogInformation("Api one call: {@Request}", request);
            var result = await _apiTwo.TwoCall(new ApiTwo.Client.Contracts.Requests.TwoRequest
            {
                Query = request.Query
            }, token);

            _logger.LogInformation("Api one call: {@Result}", result);
            return new OneResponse
            {
                Date = result.Date,
                Description = result.Description,
                Id = result.Id,
                Summary = result.Summary,
                TemperatureC = result.TemperatureC,
            };
        }

        public async IAsyncEnumerable<OneResponse> StreamCall(
            OneRequest request,
            CancellationToken token = default)
        {
            var data = Enumerable.Range(0, 100).Select(e => new OneResponse
            {
                Date = DateTime.UtcNow.AddHours(e),
                Description = $"Weather {DateTime.UtcNow.AddHours(e)}",
                Id = Guid.NewGuid(),
                Summary = "tetest",
                TemperatureC = e % 5
            });

            foreach (var item in data)
            {
                await Task.Delay(100, token);
                yield return item;
            }
        }
    }
}