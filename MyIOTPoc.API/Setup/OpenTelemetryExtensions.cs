using MyIOTPoc.API.Setup.Configuration.OpenTelemetry;
using OpenTelemetry.Exporter;
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
    /// <param name="configuration"></param>
    public static void AddOpenTelemetryLogging(
        this ILoggingBuilder logging,
        string serviceName,
        OpenTelemetryLoggingConfiguration configuration)
    {
        if (!configuration.Enabled)
            return;
        
        logging.AddOpenTelemetry(options =>
        {
            options
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName));
            if (configuration.ConsoleExporterOptions.Enabled)
                options.AddConsoleExporter();
            if(configuration.OtlpExporterOptions.Enabled)
                options.AddOtlpExporter();
        });
    }

    /// <summary>
    /// Adds OpenTelemetry tracing and metrics to the service collection.
    /// Will add traces. These traces are called activities in .NET as they are built on top of System.Diagnostics.
    /// Traces are exported using the OTLP protocol and can be collected in Jaeger.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceName">The name of service for which to create Otl</param>
    /// <param name="tracingConfiguration">Tracing configuration</param>
    /// <param name="metricsConfiguration">Metrics configuration</param>
    /// <returns></returns>
    public static IServiceCollection AddOpenTelemetryTracingAndMetrics(
        this IServiceCollection services,
        string serviceName,
        OpenTelemetryTracingConfiguration tracingConfiguration,
        OpenTelemetryMetricsConfiguration metricsConfiguration)
    {
        var resource = services.AddOpenTelemetry()
            .ConfigureResource(resource =>
                resource.AddService(serviceName));

        if (tracingConfiguration.Enabled)
            resource.WithTracing(tracing => {
                tracing
                    .AddAspNetCoreInstrumentation();
                if (tracingConfiguration.ConsoleExporterOptions.Enabled)
                    tracing.AddConsoleExporter();
                if (tracingConfiguration.OtlpExporterOptions.Enabled)
                    tracing.AddOtlpExporter();
            });

        if (metricsConfiguration.Enabled)
            resource.WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation();
                if (metricsConfiguration.ConsoleExporterOptions.Enabled)
                    metrics.AddOtlpExporter();
                if (metricsConfiguration.OtlpExporterOptions.Enabled)
                    metrics.AddOtlpExporter();
            });

        services.AddSingleton(new ActivitySource(serviceName));

        return services;
    }
}