using System;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRChat.net
{
    class Program
    {
        static void Main(string[] args)
        {
            var hubConnection = new HubConnection("http://localhost:1599");
            var chat = hubConnection.CreateHubProxy("chat");

            var currentRoom = "Lobby";

            chat.On("addMessage", (string room,string message) => {
                if( room == currentRoom ) 
                    Console.WriteLine("Received : " + message);
            });

            chat.On("addChatRoom", room => Console.WriteLine("Room added : "+room));

            hubConnection.Start().Wait();

            Console.WriteLine("Connected");

            chat.Invoke("Join", currentRoom).Wait();

            var line = string.Empty;
            while ((line = Console.ReadLine()) != null)
            {
                if (line.StartsWith("/create"))
                {
                    var room = line.Substring("/create".Length + 1);
                    chat.Invoke("CreateChatRoom", room);
                }
                if (line.StartsWith("/join"))
                {
                    var room = line.Substring("/join".Length + 1);
                    chat.Invoke("Join", room).Wait();
                    currentRoom = room;
                }
                else
                {
                    chat.Invoke("Send", currentRoom, line).Wait();
                }
            }
        }
    }
}


