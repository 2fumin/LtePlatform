using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace System.Web.Http.Owin.ExceptionHandling
{
    internal class DefaultExceptionHandler : IExceptionHandler
    {
        private static void Handle(ExceptionHandlerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            ExceptionContext exceptionContext = context.ExceptionContext;
            HttpRequestMessage request = exceptionContext.Request;
            if (request == null)
            {
                throw new ArgumentException(Error.Format(Properties.Resources.TypePropertyMustNotBeNull, 
                    typeof(ExceptionContext).Name, "Request"), nameof(context));
            }
            context.Result = new ResponseMessageResult(request.CreateErrorResponse(HttpStatusCode.InternalServerError, 
                exceptionContext.Exception));
        }

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            Handle(context);
            return TaskHelpers.Completed();
        }
    }
}
