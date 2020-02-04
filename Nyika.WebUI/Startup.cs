using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nyika.WebUI.Startup))]
namespace Nyika.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
