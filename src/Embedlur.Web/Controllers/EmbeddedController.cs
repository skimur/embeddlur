using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Embedlur.Helpers;
using Embedlur.Web.Models;

namespace Embedlur.Web.Controllers
{
    public class EmbeddedController : Controller
    {
        private readonly IProviderResolver _providerResolver;

        public EmbeddedController(IProviderResolver providerResolver)
        {
            _providerResolver = providerResolver;
        }

        public EmbeddedController()
        {
            _providerResolver = EmbedlurContext.Resolver;
        }

        public ActionResult Embed(string url = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                // for testing
                // twitter
                //url = "https://twitter.com/SHAQ/status/661263631045238784";
                // youtube
                url = "https://www.youtube.com/watch?v=xjS6SftYQaQ";
                // imgur video gallery
                url = "http://imgur.com/gallery/p3MmC";
            }

            var provider = _providerResolver.Resolve(url);
            
            if(provider == null)
                throw new Exception("The url has no provider that can handle it.");
            
            var result = provider.LocalEmbed(url);

            if(result == null)
                throw new Exception("Couldn't get the embedded result from the provider.");

            return View("Providers/" + provider.Name, result);
        }

        public ActionResult Image(string url, int width, int height)
        {
            return View(new ImageModel {ImageUrl = url, Width = width, Height = height});
        }
    }
}
