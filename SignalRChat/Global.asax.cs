using System;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.AspNet.SignalR;

namespace SignalRChat
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable
                .Routes
                    .MapHubs();

            /*
            GlobalHost
                .DependencyResolver
                    .UseSqlServer(
                        "Data Source=(local);" +
                        "Initial Catalog=SignalRChat;" +
                        "Integrated Security=true");
            */

            /*
            GlobalHost
                .DependencyResolver
                    .UseRedis(
                        "localhost",
                        6379,
                        "",
                        new[] { "signalr.key" });
             */

            /*
            GlobalHost
                .DependencyResolver
                    .UseWindowsAzureServiceBus("", 1);
            */

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (Request.IsAuthenticated == true)
                {
                    var ticket = FormsAuthentication.Decrypt(
                                    Context.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    var roles = ticket.UserData.Split(';');
                    var id = new FormsIdentity(ticket);
                    Context.User = new System.Security.Principal.GenericPrincipal(id, roles);
                }
            }
        }
    }
}

