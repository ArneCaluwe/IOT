using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyIOTPoc.DAL.Context;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.DAL.Repositories;

/// <summary>
/// Repository interface for sensor operations.
/// </summary>
public interface ISensorRepository
{
    Task<IEnumerable<Sensor>> GetSensorsAsync(CancellationToken cancellationToken);
    Task<Sensor> AddSensorAsync(Sensor sensor, CancellationToken cancellationToken);
}

/// <summary>
/// Repository implementation for sensor operations.
/// </summary>
/// <param name="context"></param>
/// <param name="activitySource"></param>
public class SensorRepository(IotDbContext context, ActivitySource activitySource) : ISensorRepository
{
    private readonly IotDbContext _context = context;
    private readonly ActivitySource _activitySource = activitySource;

    public async Task<IEnumerable<Sensor>> GetSensorsAsync(CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("SensorRepository.GetSensorsAsync");
        activity?.SetTag("Repository", nameof(SensorRepository));
        activity?.SetStatus(ActivityStatusCode.Ok);
        return await _context.Sensors.ToListAsync(cancellationToken);
    }

    public async Task<Sensor> AddSensorAsync(Sensor sensor, CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("SensorRepository.AddSensorAsync");
        activity?.SetTag("Repository", nameof(SensorRepository));
        _context.Sensors.Add(sensor);
        _context.SaveChanges();
        await _context.Entry(sensor).ReloadAsync(cancellationToken);
        
        activity?.SetStatus(ActivityStatusCode.Ok);
        return sensor;
    }
}