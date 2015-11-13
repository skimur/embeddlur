using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Embedlur.Helpers;
using NUnit.Framework;

namespace Embedlur.Tests
{
    [TestFixture]
    public class HtmlParserTests
    {
        private IHtmlParser _parser;

        [Test]
        public void Can_parse_meta_tags()
        {
            var result = _parser.ParseMetaTags("<meta property=\"og:image\" content=\"http://i.imgur.com/QkblmOr.gif\" />");
            Assert.That(result,Has.Count.EqualTo(1));
            Assert.That(result[0].Content, Is.EqualTo("http://i.imgur.com/QkblmOr.gif"));
            Assert.That(result[0].Name, Is.Null.Or.Empty);
            Assert.That(result[0].Property, Is.EqualTo("og:image"));

            result = _parser.ParseMetaTags("<meta name=\"og:image\" content=\"http://i.imgur.com/QkblmOr.gif\" />");
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Content, Is.EqualTo("http://i.imgur.com/QkblmOr.gif"));
            Assert.That(result[0].Name, Is.EqualTo("og:image"));
            Assert.That(result[0].Property, Is.Null.Or.Empty);

            result = _parser.ParseMetaTags("<meta name=\"og:image\" property=\"og:image2\" content=\"http://i.imgur.com/QkblmOr.gif\" />");
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Content, Is.EqualTo("http://i.imgur.com/QkblmOr.gif"));
            Assert.That(result[0].Name, Is.EqualTo("og:image"));
            Assert.That(result[0].Property, Is.EqualTo("og:image2"));

            result = _parser.ParseMetaTags("<meta property=\"property1\" content=\"content1\" /> <meta name=\"name2\" content=\"content2\" />");
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.Null.Or.Empty);
            Assert.That(result[0].Content, Is.EqualTo("content1"));
            Assert.That(result[0].Property, Is.EqualTo("property1"));
            Assert.That(result[1].Name, Is.EqualTo("name2"));
            Assert.That(result[1].Content, Is.EqualTo("content2"));
            Assert.That(result[1].Property, Is.Null.Or.Empty);
            
            result = _parser.ParseMetaTags("<meta name=\"twitter:card\" content=\"photo\"/>");
            Assert.That(result[0].Content, Is.EqualTo("photo"));
            Assert.That(result[0].Name, Is.EqualTo("twitter:card"));
            Assert.That(result[0].Property, Is.Null.Or.Empty);
        }

        [SetUp]
        public void Setup()
        {
            _parser = new HtmlParser();
        }
    }
}
