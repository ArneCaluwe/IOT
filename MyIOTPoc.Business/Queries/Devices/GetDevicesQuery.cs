using MyIOTPoc.Domain.Models.Devices;

namespace MyIOTPoc.Business.Queries.Devices;

/// <summary>
/// Query to get all devices.
/// </summary>
public record GetDevicesQuery() : IRequest<IEnumerable<Device>>;