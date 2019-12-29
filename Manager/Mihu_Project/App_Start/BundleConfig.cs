using System.Web;
using System.Web.Optimization;

namespace Mihu_Project
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //use this  
            bundles.Add(new StyleBundle("~/Scripts/fontawesome").Include(
                      "~/Scripts/fontawesome-free/css/all.min.css"));

            bundles.Add(new StyleBundle("~/Scripts/datatable").Include(
                       "~/Scripts/datatables/dataTables.bootstrap4.css"));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                       "~/Content/sb-admin.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryadmin").Include(
                       "~/Scripts/jquery/jquery.min.js",
                       "~/Scripts/bootstrap/js/bootstrap.bundle.min.js",
                       "~/Scripts/jquery-easing/jquery.easing.min.js",
                       "~/Scripts/sb-admin.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                     "~/Scripts/chart.js/Chart.min.js",
                     "~/Scripts/datatables/jquery.dataTables.js",
                     "~/Scripts/datatables/dataTables.bootstrap4.js",
                     "~/Scripts/demo/datatables-demo.js"));


        }
    }
}
