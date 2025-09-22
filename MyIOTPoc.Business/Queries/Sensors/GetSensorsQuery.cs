using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.Business.Queries.Sensors;

/// <summary>
/// Query to get all sensors.
/// </summary>
public record GetSensorsQuery() : IRequest<IEnumerable<Sensor>>;