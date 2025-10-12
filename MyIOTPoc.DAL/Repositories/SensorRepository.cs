using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyIOTPoc.DAL.Context;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.DAL.Repositories;

/// <summary>
/// Repository implementation for sensor operations.
/// </summary>
/// <param name="context"></param>
/// <param name="activitySource"></param>
public class SensorRepository(IotDbContext context, ActivitySource activitySource)
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
        await _context.SaveChangesAsync(cancellationToken);
        await _context.Entry(sensor).ReloadAsync(cancellationToken);

        activity?.SetStatus(ActivityStatusCode.Ok);
        return sensor;
    }

    public async Task<Reading> AddReadingAsync(Reading reading, CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("SensorRepository.AddReadingAsync");
        activity?.SetTag("Repository", nameof(SensorRepository));
        _context.Readings.Add(reading);
        await _context.SaveChangesAsync(cancellationToken);
        await _context.Entry(reading).ReloadAsync(cancellationToken);

        activity?.SetStatus(ActivityStatusCode.Ok);
        return reading;
    }
}