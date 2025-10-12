
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using DeviceBridge.Attributes;

namespace MyIOTPoc.Bridge.Commands.Sensors;

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
[JsonConvertible]
public record AddReadingCommand(
    [property:Required]
    [property:Description("Type of the sensor, e.g. temperature, humidity.")]
    string Sensor,

    [property:Required]
    [property:Description("The most recent reading for the sensor")]
    float Reading
)
{
    public static implicit operator JsonElement(AddReadingCommand command)
    {
        var json = JsonSerializer.Serialize(command);
        return JsonSerializer.Deserialize<JsonElement>(json);
    }
}