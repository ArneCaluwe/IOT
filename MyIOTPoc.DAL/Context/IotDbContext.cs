using Microsoft.EntityFrameworkCore;
using MyIOTPoc.Domain.Models.Base;
using MyIOTPoc.Domain.Models.Devices;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.DAL.Context;

public class IotDbContext(DbContextOptions<IotDbContext> options) : DbContext(options)
{

    public DbSet<Device> Devices { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<Reading> Readings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Device>().HasData(
            new Device { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Device 1", Location = "Lab", FirmwareVersion = "1.0" },
            new Device { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Device 2", Location = "Office", FirmwareVersion = "1.0" }
        );
    }

    public override int SaveChanges()
    {
        UpdateAudit();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAudit();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAudit()
    {
        var addedEntries = ChangeTracker.Entries()
            .Where(e => e.Entity is EntityBase &&
                e.State == EntityState.Added);
        foreach (var entry in addedEntries)
        {
            var entity = (EntityBase)entry.Entity;
            entity.CreatedAt = DateTime.UtcNow;
        }

        var modifiedEntries = ChangeTracker.Entries()
            .Where(e => e.Entity is EntityBase &&
                e.State == EntityState.Modified);
        foreach (var entry in modifiedEntries)
        {
            var entity = (EntityBase)entry.Entity;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
