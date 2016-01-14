using System.Web.Optimization;

namespace EHR.UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/workkerReset.css",
                "~/Content/site-structure.css",
                "~/Content/site-style.css",
                "~/Content/Refactoring.css",
                "~/Content/token-input-facebook.css",
                "~/Content/highslide.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.easing.js",
                "~/Scripts/jquery.collapse.js",
                "~/Scripts/jquery.tokeninput.js",
                "~/Scripts/jquery.corner.js",
                "~/Scripts/jquery.tmpl.js",
                "~/Scripts/jquery.alphanumeric.js",
                "~/Scripts/knockout-2.1.0.js",
                "~/Scripts/knockout-mapping.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.ui.datepicker-pt-BR.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/EHR-Script.js", "~/Scripts/highslide-with-gallery.js",
                "~/Scripts/jquery.maskedinput.min.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}