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

