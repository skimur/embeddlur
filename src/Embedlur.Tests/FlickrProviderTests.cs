using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Embedlur.Tests
{
    [TestFixture]
    public class FlickrProviderTests
    {
        private IProvider _provider;

        [Test]
        public void Can_serve_urls()
        {
            Assert.That(_provider.CanServeUrl("https://www.flickr.com/photos/oneredballoon/22450013589/in/explore-2015-11-07/"), Is.True);
            Assert.That(_provider.CanServeUrl("https://flic.kr/p/AcQbTP"), Is.True);
        }

        [Test]
        public void Can_get_html()
        {
            var result = _provider.Embed("https://www.flickr.com/photos/oneredballoon/22450013589/in/explore-2015-11-07/");
        }

        [SetUp]
        public void Setup()
        {
            _provider = EmbedlurContext.Resolver.ResolveByName("Flickr");
        }
    }
}
