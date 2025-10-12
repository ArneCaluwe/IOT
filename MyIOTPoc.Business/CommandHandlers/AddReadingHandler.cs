using MyIOTPoc.DAL.Repositories;
using MyIOTPoc.Business.Commands.Devices;
using MyIOTPoc.Domain.Models.Devices;
using Microsoft.Extensions.Logging;
using MyIOTPoc.Business.Commands.Sensors;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.Business.CommandHandlers;

/// <summary>
/// Handler for registering a new device.
/// </summary>
public class AddReadingHandler(SensorRepository sensorRepository, ActivitySource activitySource, ILogger<AddReadingHandler> logger) : IRequestHandler<AddReadingCommand, Reading>
{
    /// <summary>
    /// Handles adding a new reading.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Registered device</returns>
    public async Task<Reading> Handle(AddReadingCommand request, CancellationToken cancellationToken)
    {
        using var activity = activitySource.StartActivity("RegisterDeviceHandler.Handle");
        activity?.AddTag("Handler", GetType().Name);

        Log.AddingNewReading(logger, request);

        Reading reading = new()
        {
            Type = request.Sensor,
            Value = request.Reading
        };

        activity?.SetStatus(ActivityStatusCode.Ok);

        return await sensorRepository.AddReadingAsync(reading, cancellationToken);
    }
}

/// <summary>
/// Logger messages for AddReadingHandler.
/// </summary>
public static partial class Log
{
    /// <summary>
    /// Logs information about a new reading.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="command"></param>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Registering new reading")]
    public static partial void AddingNewReading(ILogger logger, [LogProperties(OmitReferenceName = true)] in AddReadingCommand command);

}