using MyIOTPoc.Business.Commands.Sensors;
using MyIOTPoc.Business.Queries.Sensors;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.API.Controllers;

/// <summary>
/// Extension methods to map sensor-related endpoints.
/// </summary>
public static class SensorControllerExtensions
{
    /// <summary>
    /// Maps sensor-related endpoints to the application.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication MapSensorControllers(this WebApplication app)
    {
        var group = app.MapGroup("/sensors")
            .WithTags("Sensors")
            .WithDescription("Endpoints to manage sensors in the IoT system.");

        group.MapGet("", async (ISender sender) =>
            {
                var sensors = await sender.Send(new GetSensorsQuery());
                return Results.Ok(sensors);
            })
            .Produces<List<Sensor>>(StatusCodes.Status200OK)
            .WithSummary("Get all sensors")
            .WithDescription("Retrieves a list of all sensors in the system.");

        group.MapPost("reading", async (AddReadingCommand cmd, ISender sender) =>
            {
                var reading = sender.Send(cmd);
                return Results.Created($"readings/{reading.Id}", reading);
            })
            .WithSummary("Adds a new reading")
            .WithDescription("Adds a new reading by a sensor.");

        return app;
    }
}

