using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace AppKi.Tracing
{
    public static class Di
    {
        public static IServiceCollection AddTracing(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new TracingSettings();
            configuration.GetSection("tracing").Bind(settings);
            if (!settings.Enabled)
                return services;

            return services.AddOpenTelemetryTracing((sp, builder) =>
                {
                    builder
                        .AddAspNetCoreInstrumentation(e =>
                        {
                            e.Enrich = (activity, eventName, rawObject) =>
                            {
                                activity.SetTag("event_test", eventName);
                                if (eventName.Equals("OnStartActivity"))
                                {
                                    if (rawObject is HttpRequest httpRequest)
                                    {
                                        activity.SetTag("requestProtocol", httpRequest.Protocol);
                                        activity.AddBaggage("tarceId_bag", httpRequest.HttpContext.TraceIdentifier);
                                        activity.SetCustomProperty("tarceId_prop", httpRequest.HttpContext.TraceIdentifier);
                                    }
                                }
                            };
                            //e.Filter = context =>
                            //{
                            //    var extension = context.Request.Path.Value.Split('.').LastOrDefault();
                            //    return new string[] { "js", "css", "html" }.Contains(extension)
                            //    || context.Request.Path.Value.Contains("_framework");
                            //};
                        })
                        .AddHttpClientInstrumentation()
                        .AddGrpcClientInstrumentation()
                        .AddSource(settings.ServiceName ?? "SomeService")
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(settings.ServiceName))
                        .AddJaegerExporter(options =>
                        {
                            options.AgentHost = settings.Jaeger.Host;
                            options.AgentPort = settings.Jaeger.Port;
                            options.ExportProcessorType = OpenTelemetry.ExportProcessorType.Simple;
                        });
                });
        }
    }
}