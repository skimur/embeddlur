using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;

namespace Embedlur
{
    public static class EmbedlurContext
    {
        private static readonly TinyIoCContainer Container;

        static EmbedlurContext()
        {
            Container = new TinyIoCContainer();
            Container.Register<IRestService, RestService>().AsSingleton();
            Container.Register<IProviderDiscovery, ProviderDiscovery>().AsSingleton();
            Container.Register<IProviderResolver, ProviderResolver>().AsSingleton();
        }

        public static IProviderResolver Resolver
        {
            get { return Container.Resolve<IProviderResolver>(); }
        }
    }
}
