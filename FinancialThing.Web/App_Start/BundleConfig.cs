using System.Web;
using System.Web.Optimization;

namespace FinancialThing
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.tablesorter.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/less").Include(
                        "~/Scripts/less-1.5.1.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/shared").Include(
                        "~/Scripts/shared.js",
                        "~/Scripts/dragula.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/tabledata").Include(
                "~/Scripts/datatables.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/sb-admin.css",
                      "~/Content/toastr.css",
                      "~/Content/dragula.min.cs",
                      "~/Content/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/drag").Include("~/Scripts/drag.js"));
            bundles.Add(new ScriptBundle("~/bundles/ratios").Include("~/Scripts/Ratios/Ratios.js"));
            bundles.Add(new ScriptBundle("~/bundles/d3").Include("~/Scripts/Summary/d3.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/knockout").Include("~/Scripts/knockout-3.4.0.js", "~/Scripts/knockout_helpers.js"));
            bundles.Add(new ScriptBundle("~/bundles/tooltip").Include("~/Scripts/opentip-jquery.js"));
        }
    }
}
