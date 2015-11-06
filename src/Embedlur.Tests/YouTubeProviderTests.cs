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
    public class YouTubeProviderTests
    {
        private IProvider _provider;

        [Test]
        public void Can_serve_urls()
        {
            Assert.That(_provider.CanServeUrl("https://www.youtube.com/watch?v=xjS6SftYQaQ"), Is.True);
            Assert.That(_provider.CanServeUrl("https://youtu.be/xjS6SftYQaQ"), Is.True);
        }

        [Test]
        public void Can_get_html()
        {
            var result = _provider.Embed("https://www.youtube.com/watch?v=xjS6SftYQaQ");
            result = _provider.Embed("https://youtu.be/xjS6SftYQaQ");
        }

        [SetUp]
        public void Setup()
        {
            _provider = EmbedlurContext.Resolver.ResolveByName("YouTube");
        }
    }
}
