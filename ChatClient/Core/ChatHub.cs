using Microsoft.AspNetCore.SignalR;

namespace ChatClient.Core
{
    public class ChatHub : Hub
    {
        //ik dacht om dit te doen. als het niet af is is het omdat ik er geen zin meer in had en het gewoon heb ingedient zonder dat dit werkt.
        public async Task SendMessage(string user, string message, string channel)
        {
            await Clients.Group(channel).SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinChannel(string channel)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channel);
        }

        public async Task LeaveChannel(string channel)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channel);
        }
    }

}
