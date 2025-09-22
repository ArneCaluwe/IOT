using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using MyIOTPoc.Domain.Models.Devices;

namespace MyIOTPoc.Domain.Business.Commands;

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
public record RegisterDeviceCommand(
    [property:Required]
    [property:Description("Type of the device, e.g., ArduinoUno, RaspberryPi.")]
    string DeviceType,

    [property:Required]
    [property:MinLength(1)]
    [property:Description("List of capabilities the device supports, e.g., temperature, humidity.")]
    List<string> Capabilities,

    [property:Required]
    [property:Description("Physical location of the device.")]
    string Location,
    
    [property:Required]
    [property:Description("Firmware version of the device.")]
    string FirmwareVersion
): IRequest<Device>;
