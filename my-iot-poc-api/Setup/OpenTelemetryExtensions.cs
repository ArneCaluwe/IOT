using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MyIOTPoc.API.Setup;
public static class OpenTelemetryExtensions
{
    public static void AddOpenTelemetryLogging(this ILoggingBuilder logging, string serviceName)
    {
        logging.AddOpenTelemetry(options =>
        {
            options
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName))
                .AddConsoleExporter();
        });
    }
    public static IServiceCollection AddOpenTelemetryTracingAndMetrics(this IServiceCollection services, string serviceName)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter())
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter());
        return services;
    }
}