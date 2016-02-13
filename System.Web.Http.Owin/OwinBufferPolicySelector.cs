using System.Net.Http;
using System.Web.Http.Hosting;

namespace System.Web.Http.Owin
{
    public class OwinBufferPolicySelector : IHostBufferPolicySelector
    {
        public bool UseBufferedInputStream(object hostContext)
        {
            return false;
        }

        public bool UseBufferedOutputStream(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw Error.ArgumentNull("response");
            }
            var content = response.Content;
            if (content == null)
            {
                return false;
            }
            var contentLength = content.Headers.ContentLength;
            if (contentLength.HasValue && (contentLength.Value >= 0L))
            {
                return false;
            }
            var transferEncodingChunked = response.Headers.TransferEncodingChunked;
            if (transferEncodingChunked.HasValue && transferEncodingChunked.Value)
            {
                return false;
            }
            return (!(content is StreamContent) && !(content is PushStreamContent));
        }
    }
}
