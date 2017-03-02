using System.Web.Mvc;

namespace DigitalLeader.Web.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Admin_default",
				"Admin/{controller}/{action}/{id}",
				new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "DigitalLeader.Web.Areas.Admin.Controllers" }
			);

			context.MapRoute(
				"Admin_View",
				"Admin",
				new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "DigitalLeader.Web.Areas.Admin.Controllers" }
			);

			//context.MapRoute(
			//	"Admin_default",
			//	"Admin/{controller}/{action}/{id}",
			//	new { action = "Index", id = UrlParameter.Optional },
			//	namespaces: new[] { "Raiduga.Web.Areas.Admin.Controllers" }
			//);
		}
	}
}