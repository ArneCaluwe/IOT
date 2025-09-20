using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MyIOTPoc.API.Setup;
/// <summary>
/// Extension methods for setting up OpenTelemetry logging, tracing, and metrics.
/// </summary>
public static class OpenTelemetryExtensions
{
    /// <summary>
    /// Adds OpenTelemetry logging to the logging builder.
    /// </summary>
    /// <param name="logging"></param>
    /// <param name="serviceName"></param>
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
    
    /// <summary>
    /// Adds OpenTelemetry tracing and metrics to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceName"></param>
    /// <returns></returns>
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