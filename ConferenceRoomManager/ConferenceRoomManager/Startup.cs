using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConferenceRoomManager.Startup))]
namespace ConferenceRoomManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
