using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SNAPME.Live.Startup))]
namespace SNAPME.Live
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
