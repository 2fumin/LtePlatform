using System;
using System.Runtime.ExceptionServices;

namespace Microsoft.Owin.Host.SystemWeb.Infrastructure
{
    internal class ErrorState
    {
        private readonly ExceptionDispatchInfo _exceptionDispatchInfo;

        private ErrorState(ExceptionDispatchInfo exceptionDispatchInfo)
        {
            _exceptionDispatchInfo = exceptionDispatchInfo;
        }

        public static ErrorState Capture(Exception exception)
        {
            return new ErrorState(ExceptionDispatchInfo.Capture(exception));
        }

        public void Rethrow()
        {
            _exceptionDispatchInfo.Throw();
        }
    }
}
