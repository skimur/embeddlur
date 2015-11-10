using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Embedlur
{
    public class RequestService : IRequestService
    {
        public string Get(string url, string contentType = "application/json")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = contentType;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            using (var response = (HttpWebResponse) request.GetResponse())
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        break;
                    default:
                        throw new Exception("response code is not 200 (" + response.StatusCode + ")");
                }

                using (var responseStream = response.GetResponseStream())
                {
                    if(responseStream == null) throw new Exception("Couldn't get the response stream");
                    using (var streamReader = new StreamReader(responseStream))
                        return streamReader.ReadToEnd();
                }
            }
        }
    }
}
