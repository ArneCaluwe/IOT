using System.Text.Json;
using DeviceBridge.Commands.Devices;

namespace DeviceBridge.Clients.Devices;

/// <summary>
/// A Client for http communication regarding devices.
/// </summary>
/// <param name="apiURI">the api url</param>
internal class DeviceClient(string apiURI = "http://localhost:5120")
{
    /// <summary>
    /// Sends a <see cref="RegisterDeviceCommand"/>
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> RegisterAsync(RegisterDeviceCommand command)
    {
        using var httpClient = new HttpClient();
        StringContent content = new(JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, "application/json");

        return await httpClient.PostAsync($"{apiURI}/devices/register", content);
    }
}