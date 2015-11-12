using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Embedlur.Tests
{
    [TestFixture]
    public class VimeoProviderTests
    {
        private IProvider _provider;

        [Test]
        public void Can_serve_urls()
        {
            Assert.That(_provider.CanServeUrl("https://vimeo.com/channels/mercedesbenz/130850852"), Is.True);
        }

        [Test]
        public void Can_get_result()
        {
            var result = _provider.Embed("https://vimeo.com/channels/mercedesbenz/130850852") as IVideoEmbeddedResult;
            Assert.That(result, Is.Not.Null);
        }

        [SetUp]
        public void Setup()
        {
            _provider = EmbedlurContext.Resolver.ResolveByName("Vimeo");
        }
    }
}
