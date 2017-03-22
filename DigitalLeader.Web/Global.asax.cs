namespace DigitalLeader.Web
{
	using DigitalLeader.Web.App_Start;
    using System.Threading;
    using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AutofacConfig.ConfigureContainer();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");

            AutoMapperConfig.Config();
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
