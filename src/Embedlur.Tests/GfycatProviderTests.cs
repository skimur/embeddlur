using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Embedlur.Tests
{
    [TestFixture]
    public class GfycatProviderTests
    {
        private IProvider _provider;

        [Test]
        public void Can_serve_urls()
        {
            Assert.That(_provider.CanServeUrl("http://gfycat.com/BlushingFilthyGalapagosmockingbird"), Is.True);
        }

        [Test]
        public void Can_get_result()
        {
            var result = _provider.Embed("http://gfycat.com/BlushingFilthyGalapagosmockingbird") as IVideoEmbeddedResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Can_get_local_result()
        {
            var result = _provider.LocalEmbed("http://gfycat.com/BlushingFilthyGalapagosmockingbird") as IVideoEmbeddedResult;
            Assert.That(result, Is.Not.Null);
        }

        [SetUp]
        public void Setup()
        {
            _provider = EmbedlurContext.Resolver.ResolveByName("Gfycat");
        }
    }
}
