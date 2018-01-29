using System.Web;
using System.Web.Optimization;

namespace WebPage
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Layout/css").Include(
                        "~/Scripts/jquery-ui/jquery-ui.min.css",
                        "~/Content/bootstrap-theme-superhero.css",
                        //"~/Content/bootstrap.css",
                        "~/Content/ValidationErrors.css",
                        "~/Content/bootstrap-datetimepicker.css",
                        "~/Content/bootstrap-datetimepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jsTop").Include(
                    "~/Scripts/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/Scripts/modernizr-*"));
                
            bundles.Add(new ScriptBundle("~/Layout/jsDown").Include(
                "~/Scripts/jquery-2.1.4.js",
                "~/Scripts/Site/site.js",
                "~/Scripts/jquery.inputmask/inputmask.js",
                "~/Scripts/jquery.inputmask/jquery.maskMoney.js",
                "~/Scripts/jquery.inputmask/jquery.numeric.js",
                "~/Scripts/jquery-ui/jquery-ui.js",
                "~/Scripts/jquery.inputmask/inputmask.min.js",
                "~/Scripts/jquery-ui/jquery-ui.min.js",
                "~/Scripts/jquery-ui/i18n/jquery.ui.datepicker-pt-BR.min.js",
                "~/Scripts/bootstrap.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*",
                    "~/Scripts/jquery.validate.unobtrusive*"));
            
        }
    }
}
