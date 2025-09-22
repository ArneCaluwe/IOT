namespace MyIOTPoc.API.Setup.Configuration.OpenTelemetry;

/// <summary>
/// Configuration options for OpenTelemetry exporters.
/// </summary>
/// <param name="Enabled"></param>
public record OpenTelemetryExporterOptions(bool Enabled);