namespace DigitalLeader.Web
{
    using DigitalLeader.Web.Configuration;
    using RouteLocalization.Mvc;
    using RouteLocalization.Mvc.Extensions;
    using RouteLocalization.Mvc.Setup;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.LowercaseUrls = true;

            #region Route Localization

            CultureSection cultureSection = ConfigurationManager.GetSection("SiteCultures") as CultureSection;

            string defaultCulture = "";
            ISet<string> acceptedCultures = new HashSet<string>();
            foreach (CultureElement c in cultureSection.Cultures)
            {
                acceptedCultures.Add(c.Name);
                if (c.IsDefault)
                {
                    defaultCulture = c.Name;
                }
            }

            routes.MapMvcAttributeRoutes(RouteLocalization.Mvc.Localization.LocalizationDirectRouteProvider);
            // Add translations
            // You can translate every specific route that contains default Controller and Action (which MapMvcAttributeRoutes does)
            routes.Localization(configuration =>
            {
                // Important: Set the route collection from LocalizationDirectRouteProvider if you specify your own
                //// configuration.LocalizationCollectionRoutes = provider.LocalizationCollectionRoutes;

                configuration.DefaultCulture = defaultCulture;
                configuration.AcceptedCultures = acceptedCultures;

                // Define how attribute routes should be processed:
                // * None: There will be no routes except the ones you explicitly define in Translate()
                // * AddAsNeutralRoute: Every attribute route will be added as neutral route
                // * AddAsDefaultCultureRoute: Every attribute route will be added as localized route for defined default culture
                // * AddAsNeutralAndDefaultCultureRoute: Every attribute route will be added as neutral route and
                //   as localized route for defined default culture
                // * AddAsNeutralRouteAndReplaceByFirstTranslation: Every attribute route will be added as neutral route first, but when
                //   you add a translation for a route, the neutral route will be removed
                configuration.AttributeRouteProcessing = AttributeRouteProcessing.AddAsNeutralAndDefaultCultureRoute;

                // Uncomment if you do not want the culture (en, de, ...) added to each translated route as route prefix
                configuration.AddCultureAsRoutePrefix = true;

                configuration.AddTranslationToSimiliarUrls = true;
            }).TranslateInitialAttributeRoutes().Translate(localization =>
            {
                foreach (CultureElement c in cultureSection.Cultures)
                {
                    var cultureName = c.Name;
                    if (!c.IsDefault)
                    {
                        foreach (CultureTranslationElement ct in c.Translations)
                        {
                            localization.ForCulture(cultureName)
                            .ForController(ct.Controller, ct.ControllerNamespace)
                            .ForAction(ct.Action)
                            .AddTranslation(ct.Value);
                        }
                    }
                }
            });

            // Optional
            // Setup CultureSensitiveHttpModule
            // This Module sets the Thread Culture and UICulture from http context
            // Use predefined DetectCultureFromBrowserUserLanguages delegate or implement your own
            CultureSensitiveHttpModule.GetCultureFromHttpContextDelegate =
                RouteLocalization.Mvc.Localization.DetectCultureFromBrowserUserLanguages(acceptedCultures, defaultCulture);

            // Optional
            // Add culture sensitive action filter attribute
            // This sets the Culture and UICulture when a localized route is executed
            GlobalFilters.Filters.Add(new CultureSensitiveActionFilterAttribute()
            {
                // Set this options only if you want to support detection of region dependent cultures
                // Supports this use case: https://github.com/Dresel/RouteLocalization/issues/38#issuecomment-70999613
                AcceptedCultures = acceptedCultures,
                TryToPreserverBrowserRegionCulture = true
            });

            #endregion



            routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "DigitalLeader.Web.Controllers" }
			);

		}
	}
}
