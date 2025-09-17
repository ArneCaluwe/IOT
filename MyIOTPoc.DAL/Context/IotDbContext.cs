using Microsoft.EntityFrameworkCore;
using MyIOTPoc.Domain.Models.Devices;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.DAL.Context;

public class IotDbContext(DbContextOptions<IotDbContext> options) : DbContext(options)
{

    public DbSet<Device> Devices { get; set; }
    public DbSet<Sensor> Sensors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Device>().HasData(
            new Device { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Device 1", Location = "Lab" },
            new Device { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Device 2", Location = "Office" }
        );
    }
}
