using System.Text.Json;

namespace DeviceBridge.Handlers.Base;

/// <summary>
/// Handler for <typeparamref name="TPayload"/>
/// </summary>
/// <typeparam name="TPayload"></typeparam>
public abstract class CommandHandler<TPayload>() : ICommandHandler
{
    /// <inheritdoc/>
    public string Command => typeof(TPayload).Name;

    /// <inheritdoc/>
    public void Handle(JsonElement payload)
    {
        var typedPayload = payload.Deserialize<TPayload>();
        if (typedPayload != null)
        {
            HandleTyped(typedPayload);
        }
    }

    /// <summary>
    /// Handles the Command
    /// </summary>
    /// <param name="payload"></param>
    protected abstract void HandleTyped(TPayload payload);
}