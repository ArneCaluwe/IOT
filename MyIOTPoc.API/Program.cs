using Microsoft.EntityFrameworkCore;
using MyIOTPoc.API.Controllers;
using MyIOTPoc.API.Setup;
using MyIOTPoc.DAL.Context;
using MyIOTPoc.DAL.Repositories;
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
builder.Services.AddScoped<ISensorRepository, SensorRepository>();

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


app.MapSensorControllers();
app.MapDeviceControllers();

app.Run();
