using DigitalLeader.Services.Interfaces;
using DigitalLeader.Web.Configuration;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DigitalLeader.Web.Extensions
{
	public static class RequestContextExtensions
	{
		public static int CurrectLanguageId(this RequestContext context)
		{
			var routeData = context.RouteData;
			var currentCulture = routeData.Values["culture"] == null ? "en" : routeData.Values["culture"].ToString();
			CultureSection cultureSection = ConfigurationManager.GetSection("SiteCultures") as CultureSection;

			if (cultureSection.Cultures.Count > 0)
			{
				var result = cultureSection.Cultures
					.Where(c => c.Name == currentCulture)
					.FirstOrDefault();

				if (result != null)
				{
					return result.Id;
				}
			}

			return 1;
		}
	}
}