using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

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
            _providerResolver = EmbedlurContext.Resolver;
        }

        public ActionResult Query(string url)
        {
            if(string.IsNullOrEmpty(url))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad request");

            var provider = _providerResolver.Resolve(url);

            if(provider == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "No providers found for the given url");

            var embed = provider.Embed(url);

            var result = new OEmbedJsonResult
            {
                Type = embed.Type,
                Version = embed.Version,
                Title = embed.Title,
                AuthorName = embed.AuthorName,
                AuthorUrl = embed.AuthorUrl,
                ProviderName = embed.ProviderName,
                ProviderUrl = embed.ProviderUrl,
                CacheAge = embed.CacheAge,
                ThumbnailUrl = embed.ThumbnailUrl,
                ThumbnailWidth = embed.ThumbnailWidth,
                ThumbnailHeight = embed.ThumbnailHeight
            };

            if (embed is IPhotoEmbeddedResult)
            {
                result.Url = ((IPhotoEmbeddedResult)embed).Url;
                result.Width = ((IPhotoEmbeddedResult)embed).Width;
                result.Height = ((IPhotoEmbeddedResult)embed).Height;
            }
            else if (embed is IVideoEmbeddedResult)
            {
                result.Html = ((IVideoEmbeddedResult)embed).Html;
                result.Width = ((IVideoEmbeddedResult)embed).Width;
                result.Height = ((IVideoEmbeddedResult)embed).Height;
            }
            else if (embed is IRichEmbeddedResult)
            {
                result.Html = ((IRichEmbeddedResult)embed).Html;
                result.Width = ((IRichEmbeddedResult)embed).Width;
                result.Height = ((IRichEmbeddedResult)embed).Height;
            }
            
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
