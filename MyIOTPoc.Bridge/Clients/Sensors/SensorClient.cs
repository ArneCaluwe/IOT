using System.Text.Json;
using MyIOTPoc.Bridge.Commands.Sensors;

namespace MyIOTPoc.Bridge.Clients.Sensors;

/// <summary>
/// A Client for http communication regarding sensors.
/// </summary>
/// <param name="apiURI">the api url</param>
internal class SensorClient(string apiURI = "http://localhost:5120")
{
    /// <summary>
    /// Sends a <see cref="AddReadingCommand"/>
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> AddReadingAsync(AddReadingCommand command)
    {
        using var httpClient = new HttpClient();
        StringContent content = new(JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, "application/json");

        return await httpClient.PostAsync($"{apiURI}/sensors/reading", content);
    }
}