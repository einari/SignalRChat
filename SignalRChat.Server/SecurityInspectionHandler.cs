using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SignalRChat.Server
{
    public class SecurityInspectionHandler
    {
        Func<IDictionary<string, object>, Task> _appFunc;

        public SecurityInspectionHandler(Func<IDictionary<string, object>, Task> appFunc)
        {
            _appFunc = appFunc;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            environment["server.User"] =
                new GenericPrincipal(
                    new GenericIdentity("SomeCreator"), new[] { "Creator" });
            return _appFunc.Invoke(environment);
        }
    }
}


