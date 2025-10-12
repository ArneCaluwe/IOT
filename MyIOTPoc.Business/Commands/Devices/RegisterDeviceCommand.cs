using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyIOTPoc.Domain.Models.Devices;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.Business.Commands.Devices;

/// <summary>
/// Command to register a new device in the system.
/// </summary>
/// <example>
/// {
///   "deviceType": "ArduinoUno",
///   "capabilities": ["status"],
///   "location": "Warehouse A",
///   "firmwareVersion": "1.0.3",
///   "sensors": 
/// }
/// </example>
[Description("Command to register a new device in the system.")]
public record RegisterDeviceCommand(
    [property:Required]
    [property:Description("Type of the device, e.g., ArduinoUno, RaspberryPi.")]
    string DeviceType,

    [property:Required]
    [property:MinLength(1)]
    [property:Description("List of capabilities the device supports, eg: status")]
    List<string> Capabilities,

    [property:Required]
    [property:Description("Physical location of the device.")]
    string Location,

    [property:Required]
    [property:Description("Firmware version of the device.")]
    string FirmwareVersion,

    [property: Required]
    [property:Description("List of sensors of the device.")]
    IEnumerable<Sensor> Sensors

) : IRequest<Device>;