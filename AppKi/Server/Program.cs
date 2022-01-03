using ApiOne.Client;
using AppKi.Server.Services;
using Flour.Logging;
using Flour.OTel;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLogging();

// Add services to the container.

builder.Services
    .AddControllersWithViews().Services
    .AddRazorPages().Services
    .AddApiOne(builder.Configuration)
    .AddTracing(builder.Configuration, (activity, context) =>
    {
        activity?.AddBaggage("trace.id", context.HttpContext.TraceIdentifier);
        activity?.AddBaggage("protocol.name", context.Protocol);
    })
    .AddHeaderPropagation()
    .AddCodeFirstGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseHeaderPropagation();
app.UseRouting();
app.UseGrpcWeb();


app.MapGrpcService<WeatherService>().EnableGrpcWeb();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();