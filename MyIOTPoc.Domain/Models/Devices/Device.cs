using System.ComponentModel;
using MyIOTPoc.Domain.Models.Base;

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
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The location of the device.
    /// </summary>
    [Description("The location of the device.")]
    public string Location { get; set; } = string.Empty;
}