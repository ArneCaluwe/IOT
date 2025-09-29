using System.Text.Json;

namespace DeviceBridge.Commands.Base;

/// <summary>
/// A Command wrapped in a CommandEnvelope
/// </summary>
public record CommandEnvelope(
    string Command,
    JsonElement Payload
);