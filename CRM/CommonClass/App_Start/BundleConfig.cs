using System.Web;
using System.Web.Optimization;

namespace CRM
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.1.js",
                        "~/Scripts/jqte/jquery-te-1.4.0.min.js",
                        "~/Scripts/script.js"
                        //"~/Scripts/jquery-2.1.1.intellisense.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.11.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate.*",
                       "~/Scripts/UserValidations/Validation.js",
                       "~/Scripts/jquery.unobtrusive-ajax.min.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/DateTimePicker").Include(
                       //"~/Scripts/DateTimePicker/bootstrap-datepicker.js",
                       "~/Scripts/DateTimePicker/bootstrap-datetimepicker.min.js",
                       "~/Scripts/DateTimePicker/bootstrap-timepicker.min.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/InputMask").Include(
                     "~/Scripts/InputMask/jquery.inputmask.js",
                     "~/Scripts/InputMask/jquery.inputmask.phone.extensions.js",
                     "~/Scripts/InputMask/jquery.inputmask.numeric.extensions.js",
                     "~/Scripts/InputMask/jquery.inputmask.regex.extensions.min.js",
                     "~/Scripts/InputMask/jquery.inputmask.regex.extensions.js"
                     //"~/Scripts/InputMask/jquery.inputmask.date.extensions.min.js",
                     //"~/Scripts/InputMask/jquery.inputmask.extensions.min.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/CRM").Include(
                     "~/Scripts/CRM/Company.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/DataTable").Include(
                        "~/Scripts/datatable-responsive/datatables.responsive.js",
                        "~/Scripts/datatable-responsive/TableTools/dataTables.tableTools.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/PixelAdmin").Include(
                       "~/Scripts/PixelAdmin/pixel-admin.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/Bootstrapcss").Include(
                "~/Content/assets/css/bootstrap.min.css",
                "~/Scripts/datatable-responsive/datatables.responsive.css",
                "~/Scripts/datatable-responsive/TableTools/css/dataTables.tableTools.css",
                "~/Content/assets/css/DateTimePicker/bootstrap-datetimepicker.min.css"
                ));
            
            bundles.Add(new StyleBundle("~/Content/assets/css").Include(
               "~/Content/assets/css/animate.css",
               "~/Content/assets/css/custom.css",
               "~/Content/assets/css/font-awesome.min.css",
               "~/Content/assets/css/jquery-te-1.4.0.css",
               "~/Content/assets/css/styleJSTree.css"
               ));

            bundles.Add(new StyleBundle("~/Content/JqueryUiCss").Include(
               "~/Content/themes/base/minified/jquery-ui.min.css",
               "~/Content/themes/base/minified/jquery.ui.accordion.min.css",
               "~/Content/themes/base/minified/jquery.ui.autocomplete.min.css",
               "~/Content/themes/base/minified/jquery.ui.button.min.css",
               "~/Content/themes/base/minified/jquery.ui.core.min.css",
               "~/Content/themes/base/minified/jquery.ui.datepicker.min.css",
               "~/Content/themes/base/minified/jquery.ui.dialog.min.css",
               "~/Content/themes/base/minified/jquery.ui.progressbar.min.css",
               "~/Content/themes/base/minified/jquery.ui.resizable.min.css",
               "~/Content/themes/base/minified/jquery.ui.selectable.min.css",
               "~/Content/themes/base/minified/jquery.ui.slider.min.css",
               "~/Content/themes/base/minified/jquery.ui.tabs.min.css",
               "~/Content/themes/base/minified/jquery.ui.theme.min.css"
               ));

            bundles.Add(new StyleBundle("~/Content/PixelAdmincss").Include(
               "~/Content/assets/css/pixel-admin.min.css",
               "~/Content/assets/css/widgets.min.css",
               "~/Content/assets/css/pages.min.css",
               "~/Content/assets/css/rtl.min.css",
               "~/Content/assets/css/themes.min.css",
               "~/Content/assets/css/UIRefresh.min.css",
               "~/Content/assets/css/jquery-te-1.4.0.css",
               //"~/Scripts/DateTimePicker/bootstrap-timepicker.min.js",
               "~/Content/assets/css/DateTimePicker/bootstrap-datetimepicker.min.css"
               ));
           
        }
    }
}
