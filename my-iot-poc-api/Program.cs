using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIOTPoc.API.Setup;
using MyIOTPoc.Business.Queries;
using MyIOTPoc.DAL.Context;
using MyIOTPoc.DAL.Repositories;
using MyIOTPoc.Domain.Business.Commands;
using MyIOTPoc.Domain.Models.Devices;
using MyIOTPoc.Domain.Models.Sensors;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<IotDbContext>(options =>
    options.UseInMemoryDatabase("IotDb"));

const string serviceName = "MyIOT.API";
builder.Logging.AddOpenTelemetryLogging(serviceName);
builder.Services.AddOpenTelemetryTracingAndMetrics(builder.Environment.ApplicationName);
builder.Services.AddMediatr(builder.Configuration["Licenses:Mediatr"] ?? throw new InvalidOperationException("MediatR license key is not configured"));
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

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


app.MapGet("/devices", async (ISender sender) =>
    {
        using var activity = Activity.Current;

        var devices = await sender.Send(new GetDevicesQuery());

        activity?.SetStatus(ActivityStatusCode.Ok);
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

app.MapPost("/devices/register", async (RegisterDeviceCommand cmd, ISender sender) =>
{
    using var activity = Activity.Current;

    var device = await sender.Send(cmd);

    activity?.SetStatus(ActivityStatusCode.Ok);

    return Results.Created($"/devices/{device.Id}", device);
}).Accepts<RegisterDeviceCommand>("application/json")
  .Produces<Device>(StatusCodes.Status201Created)
  .Produces(StatusCodes.Status400BadRequest)
  .WithSummary("Register a new device")
  .WithDescription("Registers a new device in the system with the provided details.");

app.Run();
