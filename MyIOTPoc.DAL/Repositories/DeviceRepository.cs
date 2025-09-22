using MyIOTPoc.DAL.Context;
using MyIOTPoc.Domain.Models.Devices;

namespace MyIOTPoc.DAL.Repositories;

/// <summary>
/// Repository interface for device operations.
/// </summary>
public interface IDeviceRepository
{
    IEnumerable<Device> GetDevices();
    Task<Device> AddDeviceAsync(Device device);
}

/// <summary>
/// Repository implementation for device operations.
/// </summary>
/// <param name="context"></param>
public class DeviceRepository(IotDbContext context) : IDeviceRepository
{
    private readonly IotDbContext _context = context;

    public IEnumerable<Device> GetDevices()
    {
        return _context.Devices;
    }

    public async Task<Device> AddDeviceAsync(Device device)
    {
        _context.Devices.Add(device);
        _context.SaveChanges();
        await _context.Entry(device).ReloadAsync();
        return device;
    }
}