using System.ComponentModel;
using MyIOTPoc.Domain.Models.Base;
using MyIOTPoc.Domain.Models.Sensors;

namespace MyIOTPoc.Domain.Models.Devices;

/// <summary>
/// Represents a device entity in the IoT system.
/// </summary>
[Description("Represents a device entity in the IoT system.")]
public class Device : EntityBase
{

    /// <summary>
    /// The name of the device.
    /// </summary>
    [Description("The name of the device.")]
    public string Name { get; init; } = default!;

    /// <summary>
    /// The firmware version of the device.
    /// </summary>
    [Description("The firmware version of the device.")]
    public string FirmwareVersion { get; init; } = default!;

    /// <summary>
    /// The location of the device.
    /// </summary>
    [Description("The location of the device.")]
    public string Location { get; init; } = default!;

    /// <summary>
    /// The capabilities of the device.
    /// </summary>
    [Description("The capabilities of the device.")]
    public IEnumerable<string> Capabilities { get; init; } = [];

    /// <summary>
    /// The Sensors for the device
    /// </summary>
    [Description("The sensors for the device")]
    public IEnumerable<Sensor> Sensors { get; init; } = [];
}