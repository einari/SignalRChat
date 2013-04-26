using System;
using Microsoft.Owin.Hosting;

namespace SignalRChat.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:8181";
            using (WebApplication.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }
}


