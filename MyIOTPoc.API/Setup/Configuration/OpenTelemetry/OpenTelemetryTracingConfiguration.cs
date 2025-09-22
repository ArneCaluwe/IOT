namespace MyIOTPoc.API.Setup.Configuration.OpenTelemetry;

/// <summary>
/// Configuration options for OpenTelemetry tracing.
/// </summary>
/// <param name="Enabled">Enables or disables OpenTelemetry Tracing.</param>
/// <param name="ConsoleExporterOptions"> Configuration options for the console exporter.</param>
/// <param name="OtlpExporterOptions"> Configuration options for the oltp exporter.</param>
public record OpenTelemetryTracingConfiguration(
    bool Enabled,
    OpenTelemetryExporterOptions ConsoleExporterOptions, 
    OpenTelemetryExporterOptions OtlpExporterOptions
);
