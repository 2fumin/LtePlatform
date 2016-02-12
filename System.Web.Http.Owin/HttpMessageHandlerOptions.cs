using System.Net.Http;
using System.Threading;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;

namespace System.Web.Http.Owin
{
    public class HttpMessageHandlerOptions
    {
        public CancellationToken AppDisposing { get; set; }

        public IHostBufferPolicySelector BufferPolicySelector { get; set; }

        public IExceptionHandler ExceptionHandler { get; set; }

        public IExceptionLogger ExceptionLogger { get; set; }

        public HttpMessageHandler MessageHandler { get; set; }
    }
}
