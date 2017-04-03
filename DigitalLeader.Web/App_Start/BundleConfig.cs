using System.Web;
using System.Web.Optimization;

namespace DigitalLeader.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			//JavaScripts

			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
					  "~/Scripts/ckeditor/ckeditor.js"));

			bundles.Add(new ScriptBundle("~/bundles/core-js").Include(
					  "~/Scripts/custom/site.js"));

			bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
					  "~/Scripts/chosen.jquery.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetime")
				.Include("~/Scripts/moment.js")
				.Include("~/Scripts/bootstrap-datetimepicker.js"));

			//CSS

			bundles.Add(new StyleBundle("~/Content/core-css")
				.Include("~/Content/bootstrap/bootstrap.css")
				.Include("~/Content/font-awesome.css"));

			bundles.Add(new StyleBundle("~/Content/chosen")
				.Include("~/Content/bootstrap/bootstrap-chosen.css"));

			//LESS

			bundles.Add(new LessBundle("~/Content/core-less")
				.Include("~/Content/less/settings.less")
				.Include("~/Content/less/general.less")
				.Include("~/Content/less/navigation.less")
				.Include("~/Content/less/footer.less")
                .Include("~/Content/less/homepage.less")
                .Include("~/Content/less/contact.less")
                .Include("~/Content/less/projects.less")
                .Include("~/Content/less/cases.less")
                .Include("~/Content/less/company.less")
                .Include("~/Content/less/testimonials.less"));

			bundles.Add(new LessBundle("~/Content/bootstrap-datetimepicker")
				.Include("~/Content/bootstrap/bootstrap-datetimepicker-build.less"));

			BundleTable.EnableOptimizations = false;
		}
	}
}
