using DeviceBridge.Clients.Devices;
using DeviceBridge.Commands.Devices;
using DeviceBridge.Handlers.Base;

namespace DeviceBridge.Handlers.Devices;

/// <inheritdoc/>
public class RegisterDeviceHandler : CommandHandler<RegisterDeviceCommand>
{
    private readonly DeviceClient client = new();

    protected override async void HandleTyped(RegisterDeviceCommand payload)
    {
        Console.WriteLine($"[DeviceRegistration] device: {payload.DeviceType}, capabilities: {payload.Capabilities}");
        var result = await client.RegisterAsync(payload);
        Console.Out.WriteLine(result);
    }
}