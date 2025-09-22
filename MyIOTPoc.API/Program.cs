using Microsoft.EntityFrameworkCore;
using MyIOTPoc.API.Controllers;
using MyIOTPoc.API.Setup;
using MyIOTPoc.API.Setup.Configuration.OpenTelemetry;
using MyIOTPoc.DAL.Context;
using MyIOTPoc.DAL.Repositories;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<IotDbContext>(options =>
    options.UseInMemoryDatabase("IotDb"));

builder.Logging.AddOpenTelemetryLogging(
    builder.Environment.ApplicationName,
    builder.Configuration.GetSection("OpenTelemetry:Logging").Get<OpenTelemetryLoggingConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry logging configuration is not set"));
builder.Services.AddOpenTelemetryTracingAndMetrics(
    builder.Environment.ApplicationName,
    builder.Configuration.GetSection("OpenTelemetry:Traces").Get<OpenTelemetryTracingConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry tracing configuration is not set"),
    builder.Configuration.GetSection("OpenTelemetry:Metrics").Get<OpenTelemetryMetricsConfiguration>() ?? throw new InvalidOperationException("OpenTelemetry metrics configuration is not set")
    );
builder.Services.AddMediatr(builder.Configuration["Licenses:Mediatr"] ?? throw new InvalidOperationException("MediatR license key is not configured"));

builder.Services.AddScoped<DeviceRepository>();
builder.Services.AddScoped<SensorRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<IotDbContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.MapSensorControllers();
app.MapDeviceControllers();

app.Run();
