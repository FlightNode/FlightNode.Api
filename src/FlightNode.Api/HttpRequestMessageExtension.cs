using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Web;

namespace FlightNode.Common.Api
{

    /// <summary>
    /// Extension methods for HTTP Request.
    /// </summary>
    /// <remarks>
    /// Useful for trying to debug odd problems with incoming payloads
    /// </remarks>
    public static class HttpRequestMessageExtension
    {
        public static string ToJsonWithPayload(this HttpRequestMessage request, object payload)
        {

            return JsonConvert.SerializeObject(new
            {
                RequestUri = request.RequestUri,
                Headers = request.Headers,
                Body = payload
            });
        }
    }

}
