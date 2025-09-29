namespace MyIOTPoc.Telemetry.Configuration.OpenTelemetry;

/// <summary>
/// Configuration options for OpenTelemetry exporters.
/// </summary>
/// <param name="Enabled"></param>
public record OpenTelemetryExporterOptions(bool Enabled);

/// <summary>
/// Configuration options for OpenTelemetry Logging exporters.
/// </summary>
/// <param name="Enabled"></param>
/// <param name="Endpoint"></param>
public record OpenTelemetryLoggingExporterOptions(bool Enabled, string Endpoint) : OpenTelemetryExporterOptions(Enabled);