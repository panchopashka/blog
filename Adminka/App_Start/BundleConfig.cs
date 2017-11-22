using System.Web;
using System.Web.Optimization;

namespace Adminka
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                 "~/Scripts/jquery-{version}.js",
                 "~/Scripts/jquery-ui-{version}.js"  ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/umd/popper.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",          
                      "~/Content/Site.css"
                    ));

            bundles.Add(new StyleBundle("~/Content/jqueryCustomCss").Include(
                      "~/Content/themes/jquery-ui-1.9.2.custom.min.css"
                    ));

            bundles.Add(new StyleBundle("~/Content/jqgrid").Include(
                "~/Content/themes/base/jquery-ui.min.css",
                "~/Content/jqgrid/ui.jqgrid.css"
             ));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                    "~/Scripts/jqgrid/grid.locale-ru.js",
                    "~/Scripts/jqgrid/jquery.jqGrid.min.js",
                    "~/Scripts/jqgrid/grid.common.js",
                    "~/Scripts/jqgrid/grid.formedit.js"));

            bundles.Add(new ScriptBundle("~/bundles/tiny_mce").Include(
                    "~/Scripts/tiny_mce/tiny_mce.js"));
        }
    }
}
