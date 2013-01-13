using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRChat
{
    public class Chat : Hub
    {
        public void Send(string message)
        {
            Clients.All.addMessage(message);
        }
    }
}