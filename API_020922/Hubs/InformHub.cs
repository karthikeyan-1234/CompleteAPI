using Microsoft.AspNetCore.SignalR;

namespace API_020922.Hubs
{
    public class InformHub : Hub
    {
        //Creates a group and assigns the connection ID to the group
        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        //Acts as server to get data from one method in a client to another method (clientHandlerMethod) in client
        public Task SendMessageToGroup(string group, string clientListener, string message)
        {
            return Clients.Group(group).SendAsync(clientListener, message + " [" + Context.ConnectionId + "]");
        }
    }
}
