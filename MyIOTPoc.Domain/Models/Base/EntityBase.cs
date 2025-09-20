using System.Text.Json.Serialization;

namespace MyIOTPoc.Domain.Models.Base;

/// <summary>
/// Base class for all entities, providing common properties.
/// </summary>
public class EntityBase
{
    /// <summary>
    /// The unique identifier for the entity.
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    /// The timestamp when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The timestamp when the entity was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}