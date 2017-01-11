using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitalLeader.Startup))]
namespace DigitalLeader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
