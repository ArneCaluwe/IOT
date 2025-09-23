using MyIOTPoc.API.Controllers;
using MyIOTPoc.API.Setup;
using MyIOTPoc.API.Setup.Configuration.OpenTelemetry;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEntityFrameworkCore();

builder.Logging.AddOpenTelemetryLogging(
    builder.Environment.ApplicationName,
    builder.Configuration.GetSection("OpenTelemetry:Logging").Get<OpenTelemetryLoggingConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry logging configuration is not set"));
builder.Services.AddOpenTelemetryTracingAndMetrics(
    builder.Environment.ApplicationName,
    builder.Configuration.GetSection("OpenTelemetry:Traces").Get<OpenTelemetryTracingConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry tracing configuration is not set"),
    builder.Configuration.GetSection("OpenTelemetry:Metrics").Get<OpenTelemetryMetricsConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry metrics configuration is not set")
    );
builder.Services.AddMediatr(builder.Configuration["Licenses:Mediatr"] ?? throw new InvalidOperationException("MediatR license key is not configured"));

builder.Services.AddRepositories();

var app = builder.Build();

app.UseEntityFrameworkCore();

if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseHttpsRedirection();
}


app.MapSensorControllers();
app.MapDeviceControllers();

app.Run();
