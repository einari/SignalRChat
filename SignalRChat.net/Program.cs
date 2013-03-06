using System;
using System.Collections.Specialized;
using System.Net;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRChat.net
{
    class Program
    {
        // Uncomment this and comment out the other to use the self hosted server
        //const string Site = "http://localhost:8181";
        const string Site = "http://localhost:1599";

        static CookieContainer Authenticate(string userName, string password)
        {
            var postData = new NameValueCollection();
            postData.Add("userName", userName);
            postData.Add("password", password);

            var url = string.Format("{0}/SecurityHandler.ashx", Site);
            var webClient = new CookieAwareWebClient();
            var result = webClient.UploadValues(url, postData);

            return webClient.CookieContainer;
        }

        static void Main(string[] args)
        {
            var hubConnection = new HubConnection(Site);

            // Comment out the next line if using self hosted server
            hubConnection.CookieContainer = Authenticate("SomeCreator", "1234");
            var chat = hubConnection.CreateHubProxy("chat");

            chat["currentChatRoom"] = "Lobby";
            chat.On("addMessage", (string room,string message) => {
                if (room == (string)chat["currentChatRoom"]) 
                    Console.WriteLine("Received : " + message);
            });

            chat.On("addChatRoom", room => Console.WriteLine("Room added : "+room));

            hubConnection.Start().Wait();

            Console.WriteLine("Connected");

            chat.Invoke("Join", (string)chat["currentChatRoom"]).Wait();

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
                    chat["currentChatRoom"] = room;
                }
                else
                {
                    chat.Invoke("Send", line).Wait();
                }
            }
        }
    }
}


