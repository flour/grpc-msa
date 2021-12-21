using AppKi.Tracing;
using Flour.Logging;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLogging();

builder.Services
    .AddTracing(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCodeFirstGrpcReflection()
    .AddCodeFirstGrpc();

var app = builder.Build();

app
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGrpcService<ApiTwoService>();
        endpoints.MapCodeFirstGrpcReflectionService();
    });

app.Run();
