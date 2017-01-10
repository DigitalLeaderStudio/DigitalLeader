using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(In4Mage.Startup))]
namespace In4Mage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
