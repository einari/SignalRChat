using System;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;

namespace SignalRChat
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable
                .Routes
                    .MapConnection<ChatConnection>("chat", "/chat");
        }
    }
}

