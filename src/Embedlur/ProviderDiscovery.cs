using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Embedlur.Providers;

namespace Embedlur
{
    public class ProviderDiscovery : IProviderDiscovery
    {
        private readonly IRestService _restService;

        public ProviderDiscovery(IRestService restService)
        {
            _restService = restService;
        }

        public List<IProvider> GetAllProviders()
        {
            var result = new List<IProvider>();
            result.Add(new TwitterProvider(_restService));
            result.Add(new YouTubeProvider(_restService));
            return result;
        }
    }
}
