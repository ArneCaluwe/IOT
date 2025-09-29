namespace MyIOTPoc.Telemetry.Configuration.OpenTelemetry;

/// <summary>
/// Configuration options for OpenTelemetry metrics.
/// </summary>
/// <param name="Enabled">Enables or disables OpenTelemetry Metrics.</param>
/// <param name="ConsoleExporterOptions"> Configuration options for the console exporter.</param>
/// <param name="OtlpExporterOptions"> Configuration options for the oltp exporter.</param>
public record OpenTelemetryMetricsConfiguration(
    bool Enabled,
    OpenTelemetryExporterOptions ConsoleExporterOptions,
    OpenTelemetryExporterOptions OtlpExporterOptions
);
