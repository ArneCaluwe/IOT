using MyIOTPoc.Domain.Models.Base;

namespace MyIOTPoc.Domain.Models.Sensors;

/// <summary>
/// Represents a sensor entity in the IoT system.
/// </summary>
public class Sensor : EntityBase
{
    /// <summary>
    /// The name of the sensor.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The type of the sensor (e.g., temperature, humidity).
    /// </summary>
    public string Type { get; set; } = string.Empty;
}