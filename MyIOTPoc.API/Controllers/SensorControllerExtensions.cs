using Microsoft.EntityFrameworkCore;
using MyIOTPoc.Business.Queries.Sensors;
using MyIOTPoc.DAL.Context;
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
                using var activity = Activity.Current;

                var sensors = await sender.Send(new GetSensorsQuery());

                activity?.SetStatus(ActivityStatusCode.Ok);
                return Results.Ok(sensors);
            })
            .Produces<List<Sensor>>(StatusCodes.Status200OK)
            .WithSummary("Get all sensors")
            .WithDescription("Retrieves a list of all sensors in the system.");

        return app;
    }
}

