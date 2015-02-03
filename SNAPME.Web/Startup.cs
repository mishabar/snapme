using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SNAPME.Web.Startup))]
namespace SNAPME.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
