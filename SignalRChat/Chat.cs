using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRChat
{
    public class Chat : Hub
    {
        public void Join(string room)
        {
            Groups.Add(Context.ConnectionId, room);
        }

        public void CreateChatRoom(string room)
        {
            if (!ChatRooms.Exists(room))
            {
                ChatRooms.Add(room);
                Clients.All.addChatRoom(room);
            }
        }

        public void Send(string room, string message)
        {
            Clients.Group(room).addMessage(room, message);
        }

        public override Task OnConnected()
        {
            foreach (var room in ChatRooms.GetAll())
                Clients.Caller.addChatRoom(room);

            return base.OnConnected();
        }
    }
}



