using MyIOTPoc.Domain.Models.Base;

namespace MyIOTPoc.Domain.Models.Devices;

public class Device: EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}