using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WhoChat.Startup))]
namespace WhoChat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
