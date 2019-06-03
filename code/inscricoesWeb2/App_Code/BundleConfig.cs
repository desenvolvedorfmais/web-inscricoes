using System.Web.Optimization;

public class BundleConfig
{
    // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
    public static void RegisterBundles(BundleCollection bundles)
    {

        bundles.Add(new StyleBundle("~/bundles/bootstrap-css").Include(
            "~/node_modules/bootstrap/dist/css/bootstrap.min.css"
        ));

        bundles.Add(new ScriptBundle("~/bundles/bootstrap-js").Include(
            "~/node_modules/bootstrap/dist/js/bootstrap.min.js"
        ));

        bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/node_modules/jquery/dist/jquery.min.js"));

        bundles.Add(new StyleBundle("~/bundles/fortawesome").Include(
            "~/node_modules/@fortawesome/fontawesome-free/css/all.min.css"));


    }

}