using AppKi.Grpc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiOne.Client
{
    public static class Di
    {
        public static IServiceCollection AddApiOne(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddGRpcService<IApiOneService>(configuration, "apis:apiOne");
        }
    }
}