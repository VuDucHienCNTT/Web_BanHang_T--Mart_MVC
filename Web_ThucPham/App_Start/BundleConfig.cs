using System.Web;
using System.Web.Optimization;

namespace Web_ThucPham
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Javascript
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/Client/js/jquery-3.1.1.js",
                        "~/Assets/Client/js/jquery-1.11.1.min.js",
                        "~/Assets/Client/js/responsiveslides.min.js",
                        "~/Assets/Client/js/hover_pack.js",
                        "~/Assets/Client/js/jquery.etalage.min.js",
                        "~/Assets/Client/js/hover_pack.js"
                        ));

            //Css
            //bundles.Add(new StyleBundle("~/bundles/core").Include(
            //          "~/Assets/Client/css/bootstrap.css",
            //          "~/Assets/Client/css/style.css",
            //          "~/Assets/Client/css/etalage.css"
            //          ));
            //BundleTable.EnableOptimizations = true;
        }
    }
}
