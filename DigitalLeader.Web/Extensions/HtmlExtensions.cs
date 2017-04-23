using DigitalLeader.Services.Interfaces;
using DigitalLeader.ViewModels.Localization;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DigitalLeader.Services.Localization;
using System.Web.WebPages;

namespace DigitalLeader.Web.Extensions
{
	public static class HtmlExtensions
	{
		public static HelperResult LocalizedEditor<T, TLocalizedModelLocal>(this HtmlHelper<T> helper,
		   string name,
		   Func<int, HelperResult> localizedTemplate,
		   Func<T, HelperResult> standardTemplate,
		   bool ignoreIfSeveralStores = false)
		   where T : ILocalizedModel<TLocalizedModelLocal>
		   where TLocalizedModelLocal : ILocalizedModelLocal
		{
			return new HelperResult(writer =>
			{
				var localizationSupported = helper.ViewData.Model.Locales.Count > 1;
				if (localizationSupported)
				{
					var tabStrip = new StringBuilder();
					tabStrip.AppendLine(string.Format("<div id=\"{0}\" class=\"nav-tabs-custom nav-tabs-localized-fields\">", name));
					tabStrip.AppendLine("<ul class=\"nav nav-tabs\">");

					//default tab
					tabStrip.AppendLine("<li class=\"active\">");
					tabStrip.AppendLine(string.Format("<a data-tab-name=\"{0}-{1}-tab\" href=\"#{0}-{1}-tab\" data-toggle=\"tab\">{2}</a>",
							name,
							"standard",
							"Standard"));
					tabStrip.AppendLine("</li>");


					foreach (var locale in helper.ViewData.Model.Locales)
					{
						tabStrip.AppendLine("<li>");
						var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

						tabStrip.AppendLine(string.Format("<a data-tab-name=\"{0}-{1}-tab\" href=\"#{0}-{1}-tab\" data-toggle=\"tab\">{2}</a>",
								name,
								locale.LanguageId,
								HttpUtility.HtmlEncode(locale.LanguageName)));

						tabStrip.AppendLine("</li>");
					}
					tabStrip.AppendLine("</ul>");

					//default tab
					tabStrip.AppendLine("<div class=\"tab-content\">");
					tabStrip.AppendLine(string.Format("<div class=\"tab-pane active\" id=\"{0}-{1}-tab\">", name, "standard"));
					tabStrip.AppendLine(standardTemplate(helper.ViewData.Model).ToHtmlString());
					tabStrip.AppendLine("</div>");

					for (int i = 0; i < helper.ViewData.Model.Locales.Count; i++)
					{
						tabStrip.AppendLine(string.Format("<div class=\"tab-pane\" id=\"{0}-{1}-tab\">",
							name,
							helper.ViewData.Model.Locales[i].LanguageId));
						tabStrip.AppendLine(localizedTemplate(i).ToHtmlString());
						tabStrip.AppendLine("</div>");
					}
					tabStrip.AppendLine("</div>");
					tabStrip.AppendLine("</div>");
					writer.Write(new MvcHtmlString(tabStrip.ToString()));
				}
				else
				{
					standardTemplate(helper.ViewData.Model).WriteTo(writer);
				}
			});
		}

		/// <summary>
		/// Generate all title parts
		/// </summary>
		/// <param name="html">HTML helper</param>
		/// <param name="addDefaultTitle">A value indicating whether to insert a default title</param>
		/// <param name="part">Title part</param>
		/// <returns>Generated string</returns>
		public static MvcHtmlString Title(this HtmlHelper html, string part = "")
		{
			return MvcHtmlString.Create(html.Encode(string.Format("{0}{1}", part, Localization.Translations.Site_Name)));
		}

		public static MvcHtmlString SEO(this HtmlHelper helper)
		{
			var result = MvcHtmlString.Empty;

			var seoKey = string.Format("{0}-{1}",
				helper.ViewContext.RouteData.Values["controller"],
				helper.ViewContext.RouteData.Values["action"]);

			var service = DependencyResolver.Current.GetService<ISEOService>();
			if (service != null)
			{
				var seoEntity = service.GetByKey(seoKey);

				if (seoEntity != null)
				{
					var languageId = helper.ViewContext.RequestContext.CurrectLanguageId();

					var seoString = string.Format(
						@"<meta name=""description"" content=""{0}""><meta name=""keywords"" content=""{1}"">",
						seoEntity.GetLocalized(x => x.Description, languageId),
						seoEntity.GetLocalized(x => x.Keywords, languageId));

					result = MvcHtmlString.Create(seoString);
				}
			}

			return result;
		}
	}
}