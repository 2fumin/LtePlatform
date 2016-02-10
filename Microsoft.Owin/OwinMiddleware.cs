using System.Threading.Tasks;

namespace Microsoft.Owin
{
    public abstract class OwinMiddleware
    {
        protected OwinMiddleware(OwinMiddleware next)
        {
            Next = next;
        }

        public abstract Task Invoke(IOwinContext context);

        protected OwinMiddleware Next { get; set; }
    }
}
