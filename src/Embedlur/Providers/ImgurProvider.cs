using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Embedlur.Helpers;

namespace Embedlur.Providers
{
    public class ImgurProvider : BaseProvider
    {
        private readonly IHtmlParser _htmlParser;
        private readonly IRequestService _requestService;

        public ImgurProvider(IHtmlParser htmlParser, IRequestService requestService)
            :base("http://imgur\\.com/(gallery/)?([0-9a-zA-Z]+)")
        {
            _htmlParser = htmlParser;
            _requestService = requestService;
        }

        public override string Name { get { return "Imgur"; } }

        protected override IEmbeddedResult ProcessUrl(string url)
        {
            var responseHtml = _requestService.Get(url, "text/html");

            var metaTags = _htmlParser.ParseMetaTags(responseHtml);

            var twitterCardMeta = metaTags.FirstOrDefault(x => x.Name == "twitter:card");

            if (twitterCardMeta == null) return null;

            if (twitterCardMeta.Content == "photo")
            {
                var twitterImgSrcMeta = metaTags.FirstOrDefault(x => x.Name == "twitter:image:src");
                if (twitterImgSrcMeta == null) return null;
                if (string.IsNullOrEmpty(twitterImgSrcMeta.Content)) return null;

                var twitterImgSrcWidthMeta = metaTags.FirstOrDefault(x => x.Name == "twitter:image:width");
                if (twitterImgSrcWidthMeta == null) return null;
                if (string.IsNullOrEmpty(twitterImgSrcWidthMeta.Content)) return null;

                var twitterImgSrcHeightMeta = metaTags.FirstOrDefault(x => x.Name == "twitter:image:height");
                if (twitterImgSrcHeightMeta == null) return null;
                if (string.IsNullOrEmpty(twitterImgSrcHeightMeta.Content)) return null;

                var openGraphTitleMeta = metaTags.FirstOrDefault(x => x.Name == "og:title");

                return new PhotoEmbeddedResult(twitterImgSrcMeta.Content, 
                    twitterImgSrcWidthMeta.Content, 
                    twitterImgSrcHeightMeta.Content,
                    openGraphTitleMeta != null ? openGraphTitleMeta.Content : null,
                    "http://imgur.com/",
                    null,
                    "Imgur",
                    "http://imgur.com/");
            }

            if (twitterCardMeta.Content == "gallery")
            {
                var match = Match(url);
                var html = string.Format("<blockquote class=\"imgur-embed-pub\" lang=\"en\" data-id=\"a/{0}\"><a href=\"//imgur.com/a/{0}\">{1}</a></blockquote><script async src=\"//s.imgur.com/min/embed.js\" charset=\"utf-8\"></script>", match.Groups[2].Value, "title");
                return new RichEmbeddedResult(html,
                    "540",
                    "500",
                    "",
                    "Imgur",
                    null,
                    "Imgur",
                    "http://imgur.com/");
            }

            return null;
        }
    }
}
