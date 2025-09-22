using MyIOTPoc.Business.Queries.Devices;
using MyIOTPoc.DAL.Repositories;
using MyIOTPoc.Domain.Models.Devices;

namespace MyIOTPoc.Business.QueryHandlers.Devices;

/// <summary>
/// Handler for retrieving all devices.
/// </summary>
public class GetDevicesQueryHandler(ActivitySource activitySource, IDeviceRepository deviceRepository) 
    : QueryHandlerWithActivity<GetDevicesQuery, IEnumerable<Device>>(activitySource)
{
    private readonly IDeviceRepository _deviceRepository = deviceRepository;

    /// <inheritdoc />
    public override async Task<IEnumerable<Device>> HandleRequest(GetDevicesQuery request, CancellationToken cancellationToken)
    {
        var devices = await _deviceRepository.GetDevicesAsync();
        return devices;
    }
}