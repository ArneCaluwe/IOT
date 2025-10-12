
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.Business.Commands.Sensors;

/// <summary>
/// Command to register a new device in the system.
/// </summary>
/// <example>
/// {
///   "deviceType": "ArduinoUno",
///   "capabilities": ["temperature", "humidity"],
///   "location": "Warehouse A",
///   "firmwareVersion": "1.0.3"
/// }
/// </example>
[Description("Command to register a new device in the system.")]
public record AddReadingCommand(
    [property:Required]
    [property:Description("Type of the sensor, e.g. temperature, humidity.")]
    string Sensor,

    [property:Required]
    [property:Description("The most recent reading for the sensor")]
    float Reading
) : IRequest<Reading>;