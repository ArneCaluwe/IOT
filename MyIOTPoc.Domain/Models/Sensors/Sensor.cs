using MyIOTPoc.Domain.Models.Base;

namespace MyIOTPoc.Domain.Models.Sensors;

public class Sensor: EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}