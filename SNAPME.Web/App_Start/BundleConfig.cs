using System.Web;
using System.Web.Optimization;

namespace SNAPME.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
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
                      "~/Scripts/jquery.bxslider.min.js",
                      "~/Scripts/jquery.cookie.js",
                      "~/Scripts/bootbox.min.js",
                      "~/Scripts/fm.scrollator.jquery.js",
                      "~/Scripts/snapme.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/retina.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                      "~/Scripts/admin.js",
                      "~/Areas/Admin/Scripts/seller.js"));

            bundles.Add(new ScriptBundle("~/bundles/seller").Include(
                      "~/Scripts/admin.js",
                      "~/Areas/Seller/Scripts/seller.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/themify-icons.css",
                      "~/Content/style.css", // icomoon
                      "~/Content/font-questrial.min.css",
                      "~/Content/jquery.bxslider.css",
                      "~/Content/fm.scrollator.jquery.css",
                      "~/Content/site.min.css"));

            bundles.Add(new StyleBundle("~/Content/admin_css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/themify-icons.css",
                      "~/Content/admin.min.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = false;
#endif
        }
    }
}
