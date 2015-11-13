using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embedlur.Providers
{
    public class ImgurProvider : BaseProvider
    {
        public ImgurProvider()
            :base("http://imgur\\.com/([0-9a-zA-Z]+)$")
        {
            
        }

        public override string Name { get { return "Imgur"; } }

        protected override IEmbeddedResult ProcessUrl(string url)
        {
            return null;
        }
    }
}
