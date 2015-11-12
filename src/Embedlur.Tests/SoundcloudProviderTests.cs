using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Embedlur.Tests
{
    [TestFixture]
    public class SoundcloudProviderTests
    {
        private IProvider _provider;

        [Test]
        public void Can_serve_urls()
        {
            Assert.That(_provider.CanServeUrl("https://soundcloud.com/tydollasign/blase"), Is.True);
        }

        [Test]
        public void Can_get_result()
        {
            var result = _provider.Embed("https://soundcloud.com/tydollasign/blase") as IRichEmbeddedResult;
            Assert.That(result, Is.Not.Null);
        }

        [SetUp]
        public void Setup()
        {
            _provider = EmbedlurContext.Resolver.ResolveByName("Soundcloud");
        }
    }
}
