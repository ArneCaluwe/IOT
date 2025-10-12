using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using DeviceBridge.Handlers.Base;
using MyIOTPoc.Bridge.Commands.Base;
using MyIOTPoc.Bridge.Handlers.Base;

namespace DeviceBridge.Commands.Dispatcher;

/// <summary>
/// Dispatcher for Commands, unwraps a <see cref="CommandEnvelope"/> and 
/// dispatches the payload to the designated 
/// <see cref="CommandHandler{TPayload}"/>
/// </summary>
public class CommandDispatcher(ActivitySource activitySource, ILogger<CommandDispatcher> logger)
{
    private readonly Dictionary<string, ICommandHandler> _handlers = [];
    private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Register a new CommandHandler
    /// </summary>
    /// <param name="handler">CommandHandler to register</param>
    /// <remarks>consider using <see cref="WithHandlersFromAssembly(Assembly?)"/></remarks>
    public void RegisterHandler(ICommandHandler handler)
    {
        _handlers[handler.Command] = handler;
        Console.WriteLine($"[Dispatcher] Registered handler: {handler.Command}");

    }

    /// <summary>
    /// Register a new CommandHandler
    /// </summary>
    /// <typeparam name="THandler">Type of CommandHandler to register</typeparam>
    /// <remarks>consider using <see cref="WithHandlersFromAssembly(Assembly?)"/></remarks>
    public void RegisterHandler<THandler>() where THandler : ICommandHandler, new()
    {
        THandler handler = new();
        RegisterHandler(handler);
    }


    /// <summary>
    /// Automatically Registers all CommandHandlers in the provided assembly
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns>The chained entity</returns>
    public CommandDispatcher WithHandlersFromAssembly(Assembly? assembly = null)
    {
        if (assembly == null) assembly = Assembly.GetExecutingAssembly();
        var handlerType = typeof(ICommandHandler);

        var handlers = assembly
            .GetTypes()
            .Where(t => handlerType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(t => (ICommandHandler)Activator.CreateInstance(t, args: activitySource)!);

        foreach (var handler in handlers)
        {
            RegisterHandler(handler);
        }

        return this;
    }

    /// <summary>
    /// Dispatches a Command based on a json formatted <see cref="CommandEnvelope"/>
    /// </summary>
    /// <param name="json"></param>
    public void Dispatch(string json)
    {
        var envelope = JsonSerializer.Deserialize<CommandEnvelope>(json, jsonSerializerOptions);
        if (envelope == null) return;

        Dispatch(envelope);
    }

    /// <summary>
    /// Dispatches a command based on a <see cref="CommandEnvelope"/>
    /// </summary>
    /// <param name="envelope">the envelope</param>
    public void Dispatch(CommandEnvelope envelope)
    {

        using var activity = activitySource.StartActivity(ActivityKind.Internal);

        string command = envelope.Command;
        if (_handlers.TryGetValue(command, out var handler))
        {
            handler.Handle(envelope.Payload);
        }
        else
        {
            logger.LogInformation($"[Dispatcher] Unknown command: {command}");
        }

        activity?.SetStatus(ActivityStatusCode.Ok);
    }
}
