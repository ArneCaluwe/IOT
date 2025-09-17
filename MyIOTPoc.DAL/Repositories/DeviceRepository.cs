using MyIOTPoc.DAL.Context;
using MyIOTPoc.Domain.Models.Devices;

namespace MyIOTPoc.DAL.Repositories;

public class DeviceRepository(IotDbContext context)
{
    private readonly IotDbContext _context = context;

    public IEnumerable<Device> GetDevices()
    {
        return _context.Devices;
    }
}