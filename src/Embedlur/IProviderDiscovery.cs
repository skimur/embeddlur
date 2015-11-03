using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embedlur
{
    public interface IProviderDiscovery
    {
        List<IProvider> GetAllProviders();
    }
}
