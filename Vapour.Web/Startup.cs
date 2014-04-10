using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vapour.Web.Startup))]
namespace Vapour.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
