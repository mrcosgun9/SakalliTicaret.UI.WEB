using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SakalliTicaret.UI.WEB.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            // ScriptBundle parametre olarak virtualDirectory yolu ister
            // ~/ ile başlayıp oluşturulacak yolu belirliyoruz. ~/bundles/jquery 
            // Include metodu ile belirlenen virtualDirectory içerisine dosya eklenir.
            bundles.Add(new ScriptBundle("~/bundles/script")
                .Include("~/Content/plugins/js/jquery-1.11.3.min.js",
                    "~/Content/plugins/js/bootstrap.min.js",
                    "~/Content/plugins/js/meanmenu.js",
                    "~/Content/plugins/js/jquery.ajaxchimp.min.js",
                    "~/Content/plugins/js/wow.min.js",
                    "~/Content/plugins/js/owl.carousel.js",
                    "~/Content/plugins/js/jquery.flexslider-min.js",
                    "~/Content/plugins/js/bootstrap-dropdownhover.min.js",
                    "~/Content/plugins/js/jquery-ui.min.js",
                    "~/Content/plugins/js/validator.min.js",
                    "~/Content/plugins/js/smooth-scroll.js",
                    "~/Content/plugins/js/jquery.fancybox.min.js",
                    "~/Content/plugins/js/standalone/selectize.js",
                    "~/scripts/jquery.validate.js",
                    "~/Content/js/init.js",
                    "~/scripts/toastr.min.js",
                    "~/Content/js/prophet.js",
                    "~/scripts/jquery.mask.js",
                    "~/scripts/sakalliscript.js"));

            bundles.Add(new StyleBundle("~/bundles/css")
               .Include("~/Content/plugins/css/bootstrap.min.css",
                   "~/Content/plugins/css/jquery.fancybox.min.css",
                   "~/Content/plugins/css/animate.css",
                   "~/Content/plugins/css/owl.css",
                   "~/Content/plugins/css/flexslider.min.css",
                   "~/Content/plugins/css/selectize.css",
                   "~/Content/plugins/css/selectize.bootstrap3.css",
                   "~/Content/plugins/css/jquery-ui.min.css",
                   "~/Content/plugins/css/bootstrap-dropdownhover.min.css",
                   "~/Content/plugins/css/meanmenu.css",
                   "~/Content/css/style.css",
                   "~/Content/css/responsive.css",
                   "~/Content/css/style.css",
                   "~/Content/css/responsive.css",
                   "~/Content/css/prophet.css",
                   "~/Content/css/basket.css",
                   "~/Content/toastr.css",
                   "~/Content/css/StyleSheet.css"));
        }
    }
}