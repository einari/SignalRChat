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
            chat.On("addMessage", message => Console.WriteLine("Received : "+message));

            hubConnection.Start().Wait();

            var line = string.Empty;
            while ((line = Console.ReadLine()) != null)
            {
                chat.Invoke("Send", line).Wait();
            }
        }
    }
}


