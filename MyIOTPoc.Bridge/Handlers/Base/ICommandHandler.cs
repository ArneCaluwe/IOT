using System.Text.Json;

namespace DeviceBridge.Handlers.Base;

/// <summary>
/// Entity to unpack a payload and Handles the unpacked Command.
/// </summary>
/// <typeparam name="TPayload"></typeparam>
public interface ICommandHandler
{
    /// <summary>
    /// The Command the handler should override
    /// </summary>
    string Command { get; }

    /// <summary>
    /// Unpacks the payload and handles the unpacked Command
    /// </summary>
    /// <param name="payload"></param>
    /// <exception cref="JsonException"/>
    /// <exception cref="NotSupportedException"/>
    void Handle(JsonElement payload);
}
