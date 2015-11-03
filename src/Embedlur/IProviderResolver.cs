using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embedlur
{
    public interface IProviderResolver
    {
        IProvider Resolve(string url);
    }
}
