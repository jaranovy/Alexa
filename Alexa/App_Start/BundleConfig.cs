using System.Web;
using System.Web.Optimization;

namespace Alexa
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/jquery-3.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Content/Scripts/site.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Scripts/bootstrap.min.js",
                      "~/Content/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/Styles/bootstrap.min.css",
                      "~/Content/Styles/site.css"));
        }
    }
}
