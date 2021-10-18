using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Chat.HubConfig
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToAll(string fromUserId)
        {
            await Clients.All.SendAsync("ReceiveMessage", fromUserId);
        }

        public async Task SendMessageToCaller(string fromUserId)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", fromUserId);
        }

        public async Task SendMessageToUser(string connectionId, string fromUserId)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", fromUserId);
        }

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessageToGroup(string group, string fromUserId)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", fromUserId);
        }

        // Get Connected ConnectionId from Client App
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        // Get Disconnected ConnectionId from Client App
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }

        public string GetConnectionId() => Context.ConnectionId;
    }
}
