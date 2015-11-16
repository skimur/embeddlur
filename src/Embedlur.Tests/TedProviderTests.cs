using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Embedlur.Tests
{
    [TestFixture]
    public class TedProviderTests
    {
        private IProvider _provider;

        [Test]
        public void Can_serve_urls()
        {
            Assert.That(_provider.CanServeUrl("http://www.ted.com/talks/andreas_ekstrom_the_moral_bias_behind_your_search_results"), Is.True);
            Assert.That(_provider.CanServeUrl("https://www.ted.com/talks/andreas_ekstrom_the_moral_bias_behind_your_search_results"), Is.True);
        }

        [Test]
        public void Can_get_result()
        {
            var result = _provider.Embed("http://www.ted.com/talks/andreas_ekstrom_the_moral_bias_behind_your_search_results") as IVideoEmbeddedResult;
            Assert.That(result, Is.Not.Null);
            result = _provider.Embed("https://www.ted.com/talks/andreas_ekstrom_the_moral_bias_behind_your_search_results") as IVideoEmbeddedResult;
            Assert.That(result, Is.Not.Null);
        }

        [SetUp]
        public void Setup()
        {
            _provider = EmbedlurContext.Resolver.ResolveByName("Ted");
        }
    }
}
