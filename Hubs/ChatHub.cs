using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace buoi18.Hubs;

public class ChatHub : Hub
{
    // lưu ds connected id , name
    private static Dictionary<string, string> _onlines = new();

    public async Task Join(string userName)
    {
        // khi có kêt noi moi thì them vao ds online
        _onlines[Context.ConnectionId] = userName;
        await Clients.All.SendAsync("OnlineChanged", _onlines.Count);
    }

    public async Task SendMessage(string userName, string message)
    {
        await Clients.All.SendAsync("RecieveMessage",userName, message, DateTime.Now.ToString("HH:mm:ss"));
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // dispose component, tắt web ..
        _onlines.Remove(Context.ConnectionId);
        await Clients.All.SendAsync("OnlineChanged", _onlines.Count);

        await base.OnDisconnectedAsync(exception);

        
    }
}