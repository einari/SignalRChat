using Microsoft.AspNet.SignalR;
using Owin;

namespace SignalRChat.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.Use(typeof(SecurityInspectionHandler));
            var hubConfiguration = new HubConfiguration()
            {
                EnableCrossDomain = true,
                EnableJavaScriptProxies = true,
                EnableDetailedErrors = true,
            };
            builder.MapHubs(hubConfiguration);
        }
    }
}



