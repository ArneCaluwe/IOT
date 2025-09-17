using Microsoft.EntityFrameworkCore;
using MyIOTPoc.API.Setup;
using MyIOTPoc.API.SignalR;
using MyIOTPoc.DAL.Context;
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

builder.Services.AddSignalR();
builder.Services.AddScoped<IotSignalRHub>();

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


app.MapHub<IotSignalRHub>("/iotHub");
app.UseHttpsRedirection();


app.MapGet("/devices", async (IotDbContext db) =>
    {
        var devices = await db.Devices.ToListAsync();
        return Results.Ok(devices);
    })
    .WithSummary("Get all devices")
    .WithDescription("Retrieves a list of all devices in the system.");

app.MapGet("/sensors", async (IotDbContext db) =>
    {
        var sensors = await db.Sensors.ToListAsync();
        return Results.Ok(sensors);

    })
    .WithSummary("Get all sensors")
    .WithDescription("Retrieves a list of all sensors in the system.");


app.Run();
