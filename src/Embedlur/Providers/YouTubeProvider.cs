using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Embedlur.Providers
{
    public class YouTubeProvider : BaseProvider
    {
        private readonly IRestService _restService;

        public YouTubeProvider(IRestService restService)
            :base("https?://(?:[^\\.]+\\.)?youtube\\.com/watch/?\\?(?:.+&)?v=([a-zA-Z0-9_-]+)", "https?://youtu\\.be/([a-zA-Z0-9_-]+)")
        {
            _restService = restService;
        }

        public override string Name { get { return "YouTube"; } }

        protected override IEmbeddedResult ProcessUrl(string url)
        {
            if(!CanServeUrl(url))
                throw new Exception("Invalid url");

            int index;
            var match = Match(url, out index);
            if (match == null)
                return null;

            var id = match.Groups[1].Value;

            var result = JsonConvert.DeserializeObject<OEmbedJsonResult>(_restService.Get("http://www.youtube.com/oembed?url=" + WebUtility.UrlEncode("https://www.youtube.com/watch?v=" + id)));

            return new VideoEmbeddedResult(result.Html, result.Width, result.Height, result.Title, result.AuthorName, result.AuthorUrl, result.ProviderName, result.ProviderUrl, result.CacheAge, result.ThumbnailUrl, result.ThumbnailWidth, result.ThumbnailHeight);
        }
    }
}
