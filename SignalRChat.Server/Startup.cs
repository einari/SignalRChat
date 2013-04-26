using Owin;

namespace SignalRChat.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.Use(typeof(SecurityInspectionHandler));
            builder.MapHubs();
        }
    }
}



