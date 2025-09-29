
using MyIOTPoc.Telemetry.Setup;
using MyIOTPoc.Telemetry.Configuration.OpenTelemetry;
using DeviceBridge.Ports;
using DeviceBridge.Commands.Dispatcher;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddOpenTelemetryLogging(
    builder.Environment.ApplicationName,
    builder.Configuration.GetSection("OpenTelemetry:Logging").Get<OpenTelemetryLoggingConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry logging configuration is not set"));
builder.Services.AddOpenTelemetryTracingAndMetrics(
    builder.Environment.ApplicationName,
    builder.Configuration.GetSection("OpenTelemetry:Traces").Get<OpenTelemetryTracingConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry tracing configuration is not set"),
    builder.Configuration.GetSection("OpenTelemetry:Metrics").Get<OpenTelemetryMetricsConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry metrics configuration is not set")
    );

var app = builder.Build();

app.Run();


var commandDispatcher = new CommandDispatcher().WithHandlersFromAssembly();
