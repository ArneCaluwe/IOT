using MyIOTPoc.DAL.Repositories;
using MyIOTPoc.Business.Commands.Devices;
using MyIOTPoc.Domain.Models.Devices;
using Microsoft.Extensions.Logging;

namespace MyIOTPoc.Business.CommandHandlers;

/// <summary>
/// Handler for registering a new device.
/// </summary>
public class RegisterDeviceHandler(DeviceRepository deviceRepository, ActivitySource activitySource, ILogger<RegisterDeviceHandler> logger) : IRequestHandler<RegisterDeviceCommand, Device>
{
    /// <summary>
    /// Handles the registration of a new device.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Registered device</returns>
    public async Task<Device> Handle(RegisterDeviceCommand request, CancellationToken cancellationToken)
    {
        using var activity = activitySource.StartActivity("RegisterDeviceHandler.Handle");
        activity?.AddTag("Handler", nameof(RegisterDeviceHandler));

        Log.RegisteringDevice(logger, request.DeviceType, request.Location);

        var device = new Device
        {
            Name = request.DeviceType,
            FirmwareVersion = request.FirmwareVersion,
            Location = request.Location,
            Capabilities = request.Capabilities
        };

        activity?.SetStatus(ActivityStatusCode.Ok);

        return await deviceRepository.AddDeviceAsync(device);
    }
}

/// <summary>
/// Logger messages for RegisterDeviceHandler.
/// </summary>
public partial class Log
{
    /// <summary>
    /// Logs information about a new device registration.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="deviceType"></param>
    /// <param name="location"></param>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Registering new device: {DeviceType} at {Location}")]
    public static partial void RegisteringDevice(ILogger logger, string deviceType, string location);
}