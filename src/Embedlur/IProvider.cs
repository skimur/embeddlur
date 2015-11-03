using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embedlur
{
    /// <summary>
    /// A provider that can convert urls into an embedded object
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// All the patterns used to match urls against this provider.
        /// </summary>
        List<string> Patterns { get; }

        /// <summary>
        /// Can this provider serve embedded content for the given url?
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        bool CanServeUrl(string url);

        IEmbeddedResult Embed(string url);
    }
}
