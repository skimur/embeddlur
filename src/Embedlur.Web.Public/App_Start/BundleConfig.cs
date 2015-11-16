using System.Web;
using System.Web.Optimization;
using BundleTransformer.Core.Transformers;

namespace Embedlur.Web.Public
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var photoswipeScriptsBundle = new ScriptBundle("~/js/photoswipe").Include(
                "~/Scripts/photoswipe.js",
                "~/Scripts/photoswipe-ui-default.js");
            photoswipeScriptsBundle.Transforms.Add(new ScriptTransformer());
            bundles.Add(photoswipeScriptsBundle);
            
            var photoswipeStylesBundle = new StyleBundle("~/css/photoswipe").Include(
                "~/Content/photoswipe/photoswipe.css",
                "~/Content/photoswipe/default-skin/default-skin.css");
            photoswipeStylesBundle.Transforms.Add(new StyleTransformer());
            bundles.Add(photoswipeStylesBundle);
        }
    }
}
