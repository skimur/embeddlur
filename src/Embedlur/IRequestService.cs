using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embedlur
{
    public interface IRequestService
    {
        string Get(string url, string contentType = "application/json");
    }
}
