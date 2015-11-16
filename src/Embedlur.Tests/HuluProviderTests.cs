using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Embedlur.Providers;
using NUnit.Framework;

namespace Embedlur.Tests
{
    [TestFixture]
    public class HuluProviderTests
    {
        private IProvider _provider;

        [Test]
        public void Can_serve_urls()
        {
            Assert.That(_provider.CanServeUrl("http://www.hulu.com/watch/154344"), Is.True);
        }

        [Test]
        public void Can_get_result()
        {
            var result = _provider.Embed("http://www.hulu.com/watch/154344") as IVideoEmbeddedResult;
            Assert.That(result, Is.Not.Null);
        }

        [SetUp]
        public void Setup()
        {
            _provider = EmbedlurContext.Resolver.ResolveByName("Hulu");
        }
    }
}
