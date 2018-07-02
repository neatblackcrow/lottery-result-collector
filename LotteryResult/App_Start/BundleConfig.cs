using System.Web;
using System.Web.Optimization;

namespace LotteryResult
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
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/locales/bootstrap-datepicker.th.min.js",
                        "~/Scripts/bootstrap-datepicker-defaults.js"));

            bundles.Add(new ScriptBundle("~/bundles/ad-message-validator").Include(
                        "~/Scripts/admessage-validator.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-datepicker3.css"));

            bundles.Add(new ScriptBundle("~/bundles/custom-global").Include(
                        "~/Scripts/Site.js"));

            bundles.Add(new ScriptBundle("~/bundles/react").Include(
                        "~/Scripts/react.js",
                        "~/Scripts/react-dom.js"));

            bundles.Add(new ScriptBundle("~/bundles/result-data-entry").Include(
                        "~/Scripts/ReactComponents/ResultEntry.jsx",
                        "~/Scripts/ReactComponents/ResultCollection.jsx",
                        "~/Scripts/ReactComponents/ResultEntryInput.jsx",
                        "~/Scripts/ReactComponents/RewardTypeBox.jsx",
                        "~/Scripts/ReactComponents/RoundBox.jsx",
                        "~/Scripts/ReactComponents/ResultEntryInsert.jsx",
                        "~/Scripts/ReactComponents/ResultEntryPreload.jsx",
                        "~/Scripts/ReactComponents/ResultEntryRoot.jsx"));

            bundles.Add(new ScriptBundle("~/bundles/result-data-admin").Include(
                        "~/Scripts/ReactComponents/ResultEntry.jsx",
                        "~/Scripts/ReactComponents/ResultCollection.jsx",
                        "~/Scripts/ReactComponents/ResultCard.jsx",
                        "~/Scripts/ReactComponents/ResultAdminValidate.jsx",
                        "~/Scripts/ReactComponents/RewardTypeBox.jsx",
                        "~/Scripts/ReactComponents/RoundBox.jsx",
                        "~/Scripts/ReactComponents/ResultAdminRoot.jsx"));

            // Minify the scripts for production usages.
            // BundleTable.EnableOptimizations = true;
        }
    }
}
