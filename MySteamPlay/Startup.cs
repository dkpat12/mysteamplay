using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySteamPlay.Startup))]
namespace MySteamPlay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
