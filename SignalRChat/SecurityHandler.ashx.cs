using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Security;

namespace SignalRChat
{
    public class SecurityHandler : IHttpHandler
    {
        Dictionary<string, string> _usersAndPassword = new Dictionary<string, string>() 
        {
            { "SomeCreator", "1234" },
            { "SomeChatter", "1234" }
        };

        Dictionary<string, string[]> _usersAndRoles = new Dictionary<string, string[]>()
        {
            {
                "SomeCreator", new[] {"Creator"}
            }
        };

        public void ProcessRequest(HttpContext context)
        {
            var userName = context.Request.Form["userName"];
            var password = context.Request.Form["password"];

            if (IsValidUser(userName, password))
            {
                var roles = GetRolesForUser(userName);
                AuthenticateUser(context, userName, roles);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }

        bool IsValidUser(string userName, string password)
        {
            foreach (var user in _usersAndPassword.Keys)
                if (user.ToLowerInvariant() == userName.ToLowerInvariant())
                    if (_usersAndPassword[user] == password)
                        return true;

            return false;
        }

        string[] GetRolesForUser(string userName)
        {
            foreach (var user in _usersAndRoles.Keys)
                if (user.ToLowerInvariant() == userName.ToLowerInvariant())
                    return _usersAndRoles[user];

            return new string[0];
        }
        

        void AuthenticateUser(
                HttpContext context, 
                string userName, 
            params string[] roles)
        {
            var ticket = new FormsAuthenticationTicket(1, userName, 
                                DateTime.Now, 
                                DateTime.Now.AddMinutes(30), 
                                false, 
                                string.Join(";",roles));
            var cookieString = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieString);
            context.Response.Cookies.Add(cookie);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


