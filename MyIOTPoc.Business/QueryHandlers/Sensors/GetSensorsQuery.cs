using MyIOTPoc.Business.Queries.Sensors;
using MyIOTPoc.DAL.Repositories;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.Business.QueryHandlers.sensors;

/// <summary>
/// Handler for retrieving all sensors.
/// </summary>
public class GetSensorsQueryHandler(ActivitySource activitySource, SensorRepository sensorRepository) 
    : QueryHandlerWithActivity<GetSensorsQuery, IEnumerable<Sensor>>(activitySource)
{
    private readonly SensorRepository _sensorRepository = sensorRepository;

    /// <inheritdoc />
    public override async Task<IEnumerable<Sensor>> HandleRequest(GetSensorsQuery request, CancellationToken cancellationToken)
    {
        var sensors = await _sensorRepository.GetSensorsAsync(cancellationToken);
        return sensors;
    }
}