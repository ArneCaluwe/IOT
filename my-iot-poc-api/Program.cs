using Microsoft.EntityFrameworkCore;
using MyIOTPoc.API.Setup;
using MyIOTPoc.DAL.Context;
using MyIOTPoc.Domain.Models.Devices;
using MyIOTPoc.Domain.Models.Sensors;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<IotDbContext>(options =>
    options.UseInMemoryDatabase("IotDb"));

const string serviceName = "iot-poc-api";
builder.Logging.AddOpenTelemetryLogging(serviceName);
builder.Services.AddOpenTelemetryTracingAndMetrics(serviceName);

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IotDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.MapGet("/devices", async (IotDbContext db) =>
    {
        var devices = await db.Devices.ToListAsync();
        return Results.Ok(devices);
    })
    .Produces<List<Device>>(StatusCodes.Status200OK)
    .WithSummary("Get all devices")
    .WithDescription("Retrieves a list of all devices in the system.");

app.MapGet("/sensors", async (IotDbContext db) =>
    {
        var sensors = await db.Sensors.ToListAsync();
        return Results.Ok(sensors);

    })
    .Produces<List<Sensor>>(StatusCodes.Status200OK)
    .WithSummary("Get all sensors")
    .WithDescription("Retrieves a list of all sensors in the system.");

app.Run();
