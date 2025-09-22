using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyIOTPoc.DAL.Context;
using MyIOTPoc.Domain.Models.Devices;

namespace MyIOTPoc.DAL.Repositories;
/// <summary>
/// Repository implementation for device operations.
/// </summary>
/// <param name="context"></param>
/// <param name="activitySource"></param>
public class DeviceRepository(IotDbContext context, ActivitySource activitySource)
{
    private readonly IotDbContext _context = context;
    private readonly ActivitySource _activitySource = activitySource;

    public async Task<IEnumerable<Device>> GetDevicesAsync()
    {
        using var activity = _activitySource.StartActivity("DeviceRepository.GetDevicesAsync");
        activity?.SetTag("Repository", nameof(DeviceRepository));
        activity?.SetStatus(ActivityStatusCode.Ok);
        return await _context.Devices.ToListAsync();
    }

    public async Task<Device> AddDeviceAsync(Device device)
    {
        using var activity = _activitySource.StartActivity("DeviceRepository.AddDeviceAsync");
        activity?.SetTag("Repository", nameof(DeviceRepository));
        _context.Devices.Add(device);
        _context.SaveChanges();
        await _context.Entry(device).ReloadAsync();
        
        activity?.SetStatus(ActivityStatusCode.Ok);
        return device;
    }
}