using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Embedlur.Providers
{
    public class TwitterProvider : BaseProvider
    {
        private readonly IRestService _restService;
        // <link rel=\"alternate\" type=\"text/xml\+oembed\" href=\"([a-zA-Z0-9:\/\.\?\=])*\"
        //private readonly Regex _ombededUrlPattern = new Regex("<link rel=\\\"alternate\\\" type=\\\"text/xml\\+oembed\\\"\" href=\\\"([a-zA-Z0-9:\\/\\.\\?\\=])*\\\"");
        private readonly Regex _ombededUrlPattern = new Regex("<link rel=\\\"alternate\\\" type=\\\"text/xml\\+oembed\\\" href=\\\"(([a-zA-Z0-9:\\/\\.\\?\\=])*)\\\"");

        public TwitterProvider(IRestService restService)
            :base("https?://(?:www|mobile\\.)?twitter\\.com/(?:#!/)?[^/]+/status(?:es)?/(\\d+)/?$", "https?://t\\.co/[a-zA-Z0-9]+")
        {
            _restService = restService;
        }

        public override IEmbeddedResult ProcessUrl(string url)
        {
            var html = _restService.Get(url);

            var match = _ombededUrlPattern.Match(html);

            if (!match.Success)
                return null;

            var oembeddedUrl = match.Groups[1].Value.Replace(".xml", ".json");

            return new EmbeddedResult
            {
                Html = JsonConvert.DeserializeObject<OEmbedJsonResult>(_restService.Get(oembeddedUrl)).Html
            };
        }
    }
}
