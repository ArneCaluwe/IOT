using System.Diagnostics;
using MediatR;
using MyIOTPoc.Business.Queries;
using MyIOTPoc.DAL.Repositories;
using MyIOTPoc.Domain.Models.Devices;

namespace MyIOTPoc.Business.QueryHandlers;

/// <summary>
/// Handler for retrieving all devices.
/// </summary>
public class GetDevicesQueryHandler(ActivitySource activitySource, IDeviceRepository deviceRepository) : IRequestHandler<GetDevicesQuery, IEnumerable<Device>>
{
    private readonly ActivitySource _activitySource = activitySource;
    private readonly IDeviceRepository _deviceRepository = deviceRepository;
    /// <summary>
    /// Handles the retrieval of all devices.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<Device>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("GetDevicesQueryHandler.Handle");
        activity?.AddTag("Handler", nameof(GetDevicesQueryHandler));

        var devices = await _deviceRepository.GetDevicesAsync();

        activity?.SetStatus(ActivityStatusCode.Ok);
        return devices;
    }
}