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
        private readonly IRequestService _requestService;

        public ProviderDiscovery(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public List<IProvider> GetAllProviders()
        {
            var result = new List<IProvider>();
            result.Add(new TwitterProvider(_requestService));
            result.Add(new YouTubeProvider(_requestService));
            result.Add(new FlickrProvider(_requestService));
            return result;
        }
    }
}
