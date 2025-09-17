namespace MyIOTPoc.Domain.Models.Base;

public class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}