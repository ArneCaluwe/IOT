namespace MyIOTPoc.Telemetry.Configuration.OpenTelemetry;
/// <summary>
/// Configuration options for OpenTelemetry logging.
/// </summary>
/// <param name="Enabled">Enables or disables OpenTelemetry Logging.</param>
/// <param name="ConsoleExporterOptions"> Configuration options for the console exporter.</param>
/// <param name="OtlpExporterOptions"> Configuration options for the oltp exporter.</param>
public record OpenTelemetryLoggingConfiguration(
    bool Enabled,
    OpenTelemetryExporterOptions ConsoleExporterOptions,
    OpenTelemetryLoggingExporterOptions OtlpExporterOptions
);
