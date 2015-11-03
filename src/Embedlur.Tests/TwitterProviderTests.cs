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
    public class TwitterProviderTests
    {
        private TwitterProvider _provider;

        [Test]
        public void Can_serve_urls()
        {
            Assert.That(_provider.CanServeUrl("https://twitter.com/SHAQ/status/661263631045238784"), Is.True);
            Assert.That(_provider.CanServeUrl("https://t.co/HxqtgzbUA9"), Is.True);
        }

        [Test]
        public void Can_get_html()
        {
            var result = _provider.ProcessUrl("https://t.co/HxqtgzbUA9");
        }

        [SetUp]
        public void Setup()
        {
            _provider = new TwitterProvider(new RestService());
        }
    }
}
