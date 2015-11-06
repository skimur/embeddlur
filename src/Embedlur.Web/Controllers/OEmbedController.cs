using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Embedlur.Web.Controllers
{
    public class OEmbedController : Controller
    {
        private readonly IProviderResolver _providerResolver;

        public OEmbedController(IProviderResolver providerResolver)
        {
            _providerResolver = providerResolver;
        }

        public OEmbedController()
        {
            _providerResolver = new ProviderResolver(new ProviderDiscovery(new RestService()));
        }

        public ActionResult Query(string url)
        {
            if(string.IsNullOrEmpty(url))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad request");

            var provider = _providerResolver.Resolve(url);

            if(provider == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "No providers found for the given url");

            var result = provider.Embed(url);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
