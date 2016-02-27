using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SocialNetwork.Startup))]

namespace SocialNetwork
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
