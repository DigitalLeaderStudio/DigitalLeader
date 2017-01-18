using System.Web;
using System.Web.Optimization;

namespace DigitalLeader
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var lessBundle = new Bundle("~/Content/LessFiles").IncludeDirectory("~/Content/less", "*.less");
            lessBundle.Transforms.Add(new LessTransform(HttpRuntime.AppDomainAppPath + "/Content/less"));
            bundles.Add(lessBundle);

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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));


            //var lessBundle = new Bundle("~/Content/LessFiles");
            //lessBundle.Include("~/Content/less/navbar.less",
            //            "~/Content/less/general.less",
            //            "~/Content/less/homepage.less",
            //            "~/Content/less/company.less",
            //            "~/Content/less/service.less",
            //            "~/Content/less/contact.less",
            //            "~/Content/less/footer.less");
            //lessBundle.Transforms.Add(new CssMinify());

            //bundles.Add(new LessBundle("~/Content/lessFiles")
            //    .Include("~/Content/less/navbar.less",
            //            "~/Content/less/general.less",
            //            "~/Content/less/homepage.less",
            //            "~/Content/less/company.less",
            //            "~/Content/less/service.less",
            //            "~/Content/less/contact.less",
            //            "~/Content/less/footer.less"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
