using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using DeviceBridge.Handlers.Base;

namespace MyIOTPoc.Bridge.Handlers.Base;

/// <summary>
/// Handler for <typeparamref name="TPayload"/>
/// </summary>
/// <typeparam name="TPayload"></typeparam>
public abstract class CommandHandler<TPayload>(ActivitySource activitySource) : ICommandHandler
{
    /// <inheritdoc/>
    public string Command => typeof(TPayload).Name;
    private JsonSerializerOptions jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

    /// <inheritdoc/>
    public void Handle(JsonElement payload)
    {
        using var activity = activitySource.CreateActivity("CommandHandler", ActivityKind.Internal);
        activity?.SetTag("CommandHandler", typeof(TPayload).Name);
        var typedPayload = payload.Deserialize<TPayload>(jsonSerializerOptions);
        if (typedPayload != null)
        {
            HandleTyped(typedPayload);
            activity?.SetStatus(ActivityStatusCode.Ok);
            return;
        }
        activity?.SetStatus(ActivityStatusCode.Error);
    }

    /// <summary>
    /// Handles the Command
    /// </summary>
    /// <param name="payload"></param>
    protected abstract void HandleTyped(TPayload payload);
}