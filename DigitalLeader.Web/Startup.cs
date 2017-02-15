using Microsoft.Owin;
using Owin;

//[assembly: OwinStartupAttribute(typeof(DigitalLeader.Web.Startup))]
[assembly: OwinStartup(typeof(DigitalLeader.Web.Startup))]
namespace DigitalLeader.Web
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
