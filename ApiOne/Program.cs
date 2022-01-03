using ApiOne;
using ApiTwo.Client;
using Flour.Logging;
using Flour.OTel;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLogging();
builder.Services
    .AddTracing(builder.Configuration)
    .AddApiTwo(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCodeFirstGrpcReflection()
    .AddCodeFirstGrpc();

var app = builder.Build();
app
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGrpcService<ApiOneService>();
        endpoints.MapCodeFirstGrpcReflectionService();
    });

app.Run();
