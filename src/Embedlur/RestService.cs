using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Embedlur
{
    public class RestService : IRestService
    {
        public string Get(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "application/json";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            using (var response = (HttpWebResponse) request.GetResponse())
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new Exception("URL Not found");
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("URL Unauthorize");
                    case HttpStatusCode.NotImplemented:
                        throw new Exception("OEmbed has not been implemented yet.");
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
