using AppKi.Grpc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTwo.Client
{
    public static class Di
    {
        public static IServiceCollection AddApiTwo(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddGRpcService<IApiTwoService>(configuration, "apis:apiTwo");
        }
    }
}