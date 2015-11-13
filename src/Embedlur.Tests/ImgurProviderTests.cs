using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Embedlur.Tests
{
    [TestFixture]
    public class ImgurProviderTests
    {
        private IProvider _provider;

        [Test]
        public void Can_serve_urls()
        {
            Assert.That(_provider.CanServeUrl("http://imgur.com/gallery/lLQhNd9"), Is.True);
            Assert.That(_provider.CanServeUrl("http://imgur.com/lLQhNd9"), Is.True);
        }

        [Test]
        public void Can_get_photo_result()
        {
            var result = _provider.Embed("http://imgur.com/gallery/lLQhNd9") as IPhotoEmbeddedResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Url, Is.EqualTo("https://i.imgur.com/lLQhNd9.jpg"));
        }

        [Test]
        public void Can_get_gallery_result()
        {
            var result = _provider.Embed("http://imgur.com/gallery/p3MmC") as IRichEmbeddedResult;
            Assert.That(result, Is.Not.Null);
        }

        [SetUp]
        public void Setup()
        {
            _provider = EmbedlurContext.Resolver.ResolveByName("Imgur");
        }
    }
}
