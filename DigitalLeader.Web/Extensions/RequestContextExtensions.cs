using System.Linq;
using System.Web.Routing;

namespace DigitalLeader.Web.Extensions
{
	public static class RequestContextExtensions
	{
		public static int CurrectLanguageId(this RequestContext context)
		{
			var routeData = context.RouteData;
			var currentCulture = routeData.Values["culture"] == null ? "en" : routeData.Values["culture"].ToString();
			
			if (CommonStaticData.CulturesSection.Cultures.Count > 0)
			{
				var result = CommonStaticData.CulturesSection.Cultures
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