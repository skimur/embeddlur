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
                var result = new RichEmbeddedResult(html,
                    "540",
                    "500",
                    "",
                    "Imgur",
                    null,
                    "Imgur",
                    "http://imgur.com/");

                var galleryItems = new List<ImgurGalleryItem>();

                int? currentWidth = null;
                int? currentHeight = null;

                foreach (var openGraphImage in metaTags.Where(x => x.Property == "og:image" || x.Property == "og:image:width" || x.Property == "og:image:height")
                    // the layout in the markup is image > width > height.
                    // this reorders so dimensions are first.
                    .Reverse())
                {
                    if (openGraphImage.Property == "og:image")
                    {
                        var openGraphImageUrl = openGraphImage.Content;
                        if (string.IsNullOrEmpty(openGraphImageUrl))
                        {
                            currentWidth = null;
                            currentHeight = null;
                            continue;
                        }

                        if (!currentWidth.HasValue || !currentHeight.HasValue)
                        {
                            currentWidth = null;
                            currentHeight = null;
                            continue;
                        }

                        var galleryItem = new ImgurGalleryItem();
                        galleryItem.Url = openGraphImageUrl;
                        galleryItem.Width = currentWidth.Value;
                        galleryItem.Height = currentHeight.Value;

                        if (galleryItem.Url.EndsWith("?fb"))
                            galleryItem.Url = galleryItem.Url.Substring(0, galleryItem.Url.Length - 3);

                        if (galleryItem.Url.EndsWith(".gif"))
                        {
                            galleryItem.Type = ImgurGalleryItemType.Gif;
                            galleryItem.Mp4 = galleryItem.Url.Substring(0, galleryItem.Url.Length - 3) + "mp4";
                            galleryItem.Webm = galleryItem.Url.Substring(0, galleryItem.Url.Length - 3) + "webm";
                        }
                        else
                        {
                            galleryItem.Type = ImgurGalleryItemType.Photo;
                        }

                        galleryItems.Add(galleryItem);

                        currentWidth = null;
                        currentHeight = null;
                    }
                    else if (openGraphImage.Property == "og:image:width")
                    {
                        int temp;
                        if (int.TryParse(openGraphImage.Content, out temp))
                            currentWidth = temp;
                    }
                    else if (openGraphImage.Property == "og:image:height")
                    {
                        int temp;
                        if (int.TryParse(openGraphImage.Content, out temp))
                            currentHeight = temp;
                    }
                }

                if (galleryItems.Count > 0)
                {
                    result.AdditionalData["Items"] = galleryItems;
                }

                return result;
            }

            return null;
        }

        public class ImgurGalleryItem
        {
            public ImgurGalleryItemType Type { get; set; }

            public string Url { get; set; }

            public string Mp4 { get; set; }

            public string Webm { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }
        }

        public enum ImgurGalleryItemType
        {
            Photo,
            Gif
        }
    }
}
