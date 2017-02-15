﻿namespace DigitalLeader.Web
{
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;

	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.LowercaseUrls = true;
			routes.MapMvcAttributeRoutes();

			routes.MapRoute(
				"RootPages",
				"{action}",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				new { isMethodInHomeController = new RootRouteConstraint<HomeController>() }
				);

			routes.MapRoute(
				"BlogCategoriesRoute",
				 "Admin/Blogpost/Category/{action}/{id}",
				new { controller = "Category", action = "Index", id = UrlParameter.Optional }
				);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);

		}
	}

	public class RootRouteConstraint<T> : IRouteConstraint
	{
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			var rootMethodNames = typeof(T).GetMethods().Select(x => x.Name.ToLower());
			return rootMethodNames.Contains(values["action"].ToString().ToLower());
		}
	}
}
