using System.Web;
using System.Web.Optimization;

namespace Embedlur.Web.Public
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/photoswipe").Include(
                 "~/Scripts/photoswipe.js",
                 "~/Scripts/photoswipe-ui-default.js"));

            bundles.Add(new StyleBundle("~/css/photoswipe").Include(
                 "~/Content/photoswipe/photoswipe.css",
                 "~/Content/photoswipe/default-skin/default-skin.css"));
        }
    }
}
