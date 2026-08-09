using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace buoi18.Hubs;

public class VoteHub : Hub
{
    // biến static dùng chung cho tất vả kết nối/ client/ máy khách
    private static int _votes;
    // tăng số lượng vote
    public async Task Vote()
    {
        // tăn vote 
        _votes = _votes + 1;
        // gọi lại thằng 
        await Clients.All.SendAsync("VoteChanged",_votes);
        Console.WriteLine("[ID-Connected]-" + Context.ConnectionId);
    }

    public async Task WriteBoard(string content)
    {
        Console.WriteLine("Content" + content);
        await Clients.All.SendAsync("ReceiveBoard",Context.ConnectionId, content);
    }   
}