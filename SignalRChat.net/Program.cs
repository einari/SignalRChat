using Microsoft.AspNet.SignalR.Client;
using System;

namespace SignalRChat.net
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new Connection("http://localhost:1599/chat");
            connection.Received += data => Console.WriteLine("Received : "+data);
            connection.Start().ContinueWith(t=>Console.WriteLine("Connected")).Wait();

            var line = string.Empty;
            while ((line = Console.ReadLine()) != null)
            {
                connection.Send(line).Wait();
            }
        }
    }
}


