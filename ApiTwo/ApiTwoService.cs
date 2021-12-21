using ApiTwo.Client;
using ApiTwo.Client.Contracts.Requests;
using ApiTwo.Client.Contracts.Responses;
using System.Diagnostics;

internal class ApiTwoService : IApiTwoService
{
    private readonly ActivitySource _activity = new(nameof(ApiTwoService));
    private readonly ILogger<ApiTwoService> _logger;

    public ApiTwoService(ILogger<ApiTwoService> logger)
    {
        _logger = logger;
    }

    public ValueTask<TwoResponse> TwoCall(
        TwoRequest request,
        CancellationToken token = default)
    {
        using var activity = _activity.StartActivity("Two call", ActivityKind.Server);
        activity?
            .AddTag("query", request.Query);

        _logger.LogInformation("Api two query: {Query}", request.Query);

        return ValueTask.FromResult(new TwoResponse
        {
            Date = DateTime.UtcNow,
            Description = "Desc",
            Id = Guid.NewGuid(),
            Summary = "Summary",
            TemperatureC = 25m,
        });
    }
}