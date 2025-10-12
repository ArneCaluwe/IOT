using MyIOTPoc.Domain.Models.Base;

namespace MyIOTPoc.Domain.Models.Sensors;

/// <summary>
/// Represents a sensor entity in the IoT system.
/// </summary>
public class Reading : EntityBase
{
    /// <summary>
    /// The type of the reading (e.g., temperature, humidity).
    /// </summary>
    public string Type { get; set; } = default!;

    /// <summary>
    /// The Value of the reading
    /// </summary>
    public float Value { get; set; } = default!;
}