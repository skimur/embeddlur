using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Embedlur.Providers
{
    public abstract class BaseProvider : IProvider
    {
        private readonly List<string> _patterns;

        protected BaseProvider(params string[] patterns)
        {
            _patterns = patterns.ToList();
        }

        public List<string> Patterns { get { return _patterns.ToList(); } }

        public bool CanServeUrl(string url)
        {
            return _patterns.Any(pattern => Regex.IsMatch(url, pattern));
        }

        public IEmbeddedResult Embed(string url)
        {
            if(!CanServeUrl(url))
                throw new Exception("The given url is invalid.");

            return ProcessUrl(url);
        }

        public abstract IEmbeddedResult ProcessUrl(string url);
    }
}
