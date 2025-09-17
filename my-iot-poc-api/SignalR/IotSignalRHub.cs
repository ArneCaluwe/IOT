using Microsoft.AspNetCore.SignalR;

namespace MyIOTPoc.API.SignalR;

public class IotSignalRHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}