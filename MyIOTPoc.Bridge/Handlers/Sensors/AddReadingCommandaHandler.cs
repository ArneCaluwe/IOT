using System.Diagnostics;
using MyIOTPoc.Bridge.Clients.Sensors;
using MyIOTPoc.Bridge.Commands.Sensors;
using MyIOTPoc.Bridge.Handlers.Base;

namespace MyIOTPoc.Bridge.Handlers.Sensors;

/// <inheritdoc/>
public class AddReadingCommandHandler(ActivitySource activitySource) : CommandHandler<AddReadingCommand>(activitySource)
{
    private readonly SensorClient client = new();

    protected override async void HandleTyped(AddReadingCommand payload)
    {
        Console.WriteLine($"[AddReading] sensor: {payload.Sensor}, reading: {payload.Reading}");
        var result = await client.AddReadingAsync(payload);
        Console.Out.WriteLine(result);
    }
}