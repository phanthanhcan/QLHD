using System.Web;
using System.Web.Optimization;

namespace HopDongMgr
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bundle CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(

                        "~/Content/dist/css/AdminLTE.css",
                        "~/Content/dist/css/skins/skin-blue.min.css",
                        "~/Content/bootstrap.min.css",
                        "~/Content/font-awesome.css",
                        "~/Content/ionicons.min.css",
                        "~/Content/jstree/themes/default/style.min.css",
                        "~/Content/tabulator/themes/tabulator_custom.css",
                        "~/Content/ionicons.min.css",
                        "~/Content/plugins/select2/select2.css",
                        "~/Content/Site.css",
                        "~/Content/toggleable-button.css",
                        "~/Content/plugins/bootstrap-switch.css",
                        "~/Content/bootstrap-select.css"
                ));
            bundles.Add(new StyleBundle("~/Content/DataTables/css/css").Include(
                     "~/Content/DataTables/media/css/jquery.dataTables.min.css"
                    ,"~/Content/DataTables/media/css/dataTables.bootstrap4.min.css"
              ));
            bundles.Add(new StyleBundle("~/Content/Datepicker/css").Include(
                    "~/Content/Datepicker/jquery-ui.css",
                    "~/Content/Datepicker/jquery-ui.theme.min.css"
                ));

            // Bundle Javascript
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validateCanpt.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.bundle.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/AdminLTE").Include(
                        "~/Content/dist/js/app.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                        "~/Scripts/respond.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                        "~//Content/Datepicker/jquery-ui.js",
                        "~/Content/Datepicker/datepicker-vi.js",
                        "~/Scripts/bootstrap-notify.min.js",
                        "~/Scripts/tabulator.js",
                        "~/Scripts/bootstrap-waitingfor.js",
                        "~/Content/plugins/chartjs/Chart.min.js",
                        "~/Content/plugins/select2/select2.full.min.js",
                        "~/Scripts/Plugins/bootstrap-switch.js",
                        "~/Scripts/Plugins/jquery.masknumber.js",
                        "~/Scripts/bootstrap-select.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/private").Include(
                        "~/Scripts/IsoMgrJS.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables/media/js/jquery.dataTables.js"
                       ,"~/Scripts/DataTables/media/js/dataTables.bootstrap4.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                        //"~/Content/plugins/bootstrap-select-1.12.4/js/bootstrap-select.js"
    ));
            //---
        }
    }
}
