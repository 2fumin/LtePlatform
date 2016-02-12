using System;
using System.Threading;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal class CallContextAsyncResult : IAsyncResult
    {
        private AsyncCallback _callback;
        private readonly IDisposable _cleanup;
        private ErrorState _errorState;
        private volatile bool _isCompleted;
        private static readonly AsyncCallback NoopAsyncCallback = delegate {
        };
        private static readonly AsyncCallback SecondAsyncCallback = delegate {
        };
        private static readonly ITrace Trace = TraceFactory.Create(TraceName);
        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.CallContextAsyncResult";

        internal CallContextAsyncResult(IDisposable cleanup, AsyncCallback callback, object extraData)
        {
            _cleanup = cleanup;
            _callback = callback ?? NoopAsyncCallback;
            AsyncState = extraData;
        }
        
        public void Complete(bool completedSynchronously, ErrorState errorState)
        {
            _errorState = errorState;
            CompletedSynchronously = completedSynchronously;
            _isCompleted = true;
            try
            {
                Interlocked.Exchange(ref _callback, SecondAsyncCallback)(this);
            }
            catch (Exception exception)
            {
                Trace.WriteError(Resources.Trace_OwinCallContextCallbackException, exception);
            }
        }

        public static void End(IAsyncResult result)
        {
            CallContextAsyncResult result2 = result as CallContextAsyncResult;
            if (result2 == null)
            {
                throw new ArgumentException(string.Empty, nameof(result));
            }
            result2._cleanup?.Dispose();
            result2._errorState?.Rethrow();
            if (!result2.IsCompleted)
            {
                throw new ArgumentException(string.Empty, nameof(result));
            }
        }

        public object AsyncState { get; }

        public WaitHandle AsyncWaitHandle => null;

        public bool CompletedSynchronously { get; private set; }

        public bool IsCompleted => _isCompleted;
    }
}
