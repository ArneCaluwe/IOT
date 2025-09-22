using System.Diagnostics;
using MediatR;
using MyIOTPoc.DAL.Repositories;
using MyIOTPoc.Domain.Business.Commands;
using MyIOTPoc.Domain.Models.Devices;

namespace MyIOTPoc.Business.CommandHandlers;

/// <summary>
/// Handler for registering a new device.
/// </summary>
public class RegisterDeviceHandler(IDeviceRepository deviceRepository, ActivitySource activitySource) : IRequestHandler<RegisterDeviceCommand, Device>
{
    private readonly IDeviceRepository _deviceRepository = deviceRepository;
    private readonly ActivitySource _activitySource = activitySource;

    /// <summary>
    /// Handles the registration of a new device.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Registered device</returns>
    public async Task<Device> Handle(RegisterDeviceCommand request, CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("RegisterDeviceHandler.Handle");
        activity?.AddTag("Handler", nameof(RegisterDeviceHandler));

        var device = new Device
        {
            Name = request.DeviceType,
            FirmwareVersion = request.FirmwareVersion,
            Location = request.Location,
            Capabilities = request.Capabilities
        };

        activity?.SetStatus(ActivityStatusCode.Ok);

        return await _deviceRepository.AddDeviceAsync(device);
    }
}