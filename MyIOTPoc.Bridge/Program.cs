
using MyIOTPoc.Telemetry.Setup;
using MyIOTPoc.Telemetry.Configuration.OpenTelemetry;
using MyIOTPoc.Bridge.BackgroundServices;
using DeviceBridge.Commands.Dispatcher;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddOpenTelemetryLogging(
    builder.Environment.ApplicationName,
    builder.Configuration.GetSection("OpenTelemetry:Logging").Get<OpenTelemetryLoggingConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry logging configuration is not set"));
builder.Services.AddOpenTelemetryTracingAndMetrics(
    builder.Environment.ApplicationName,
    builder.Configuration.GetSection("OpenTelemetry:Traces").Get<OpenTelemetryTracingConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry tracing configuration is not set"),
    builder.Configuration.GetSection("OpenTelemetry:Metrics").Get<OpenTelemetryMetricsConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry metrics configuration is not set")
    );

builder.Services.AddSingleton((sp) => new CommandDispatcher(sp.GetRequiredService<ActivitySource>(), sp.GetRequiredService<ILogger<CommandDispatcher>>()).WithHandlersFromAssembly());
builder.Services.AddHostedService<SerialPortListenerService>();

var app = builder.Build();

app.Run();

