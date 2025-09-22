using MyIOTPoc.Business.Commands.Devices;
using MyIOTPoc.Business.Queries.Devices;
using MyIOTPoc.Domain.Models.Devices;

namespace MyIOTPoc.API.Controllers;

/// <summary>
/// Extension methods to map device-related endpoints.
/// </summary>
public static class DeviceControllerExtensions
{
    /// <summary>
    /// Maps device-related endpoints to the application.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication MapDeviceControllers(this WebApplication app)
    {
        var group = app.MapGroup("/devices")
            .WithTags("Devices");


        group.MapGet("", async (ISender sender) =>
            {
                var devices = await sender.Send(new GetDevicesQuery());
                return Results.Ok(devices);
            })
            .Produces<List<Device>>(StatusCodes.Status200OK)
            .WithSummary("Get all devices")
            .WithDescription("Retrieves a list of all devices in the system.");


        group.MapPost("register", async (RegisterDeviceCommand cmd, ISender sender) =>
        {
            var device = await sender.Send(cmd);
            return Results.Created($"/devices/{device.Id}", device);
        }).Accepts<RegisterDeviceCommand>("application/json")
          .Produces<Device>(StatusCodes.Status201Created)
          .Produces(StatusCodes.Status400BadRequest)
          .WithSummary("Register a new device")
          .WithDescription("Registers a new device in the system with the provided details.");

        return app;

    }
}