using System;
using System.Threading;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;

namespace Microsoft.Owin.Host.SystemWeb.IntegratedPipeline
{
    internal class StageAsyncResult : IAsyncResult
    {
        private readonly AsyncCallback _callback;
        private readonly Action _completing;
        private int _completions;
        private ErrorState _error;
        private volatile int _managedThreadId = Thread.CurrentThread.ManagedThreadId;

        public StageAsyncResult(AsyncCallback callback, object extradata, Action completing)
        {
            _callback = callback;
            AsyncState = extradata;
            _completing = completing;
        }

        public static void End(IAsyncResult ar)
        {
            StageAsyncResult result = (StageAsyncResult)ar;
            if (!result.IsCompleted)
            {
                throw new NotImplementedException();
            }
            result._error?.Rethrow();
        }

        public void Fail(ErrorState error)
        {
            _error = error;
            TryComplete();
        }

        public void InitialThreadReturning()
        {
            _managedThreadId = -2147483648;
        }

        public void TryComplete()
        {
            if (Interlocked.Increment(ref _completions) == 1)
            {
                if (_managedThreadId == Thread.CurrentThread.ManagedThreadId)
                {
                    CompletedSynchronously = true;
                }
                IsCompleted = true;
                _completing();
                _callback?.Invoke(this);
            }
        }

        public object AsyncState { get; }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool CompletedSynchronously { get; private set; }

        public bool IsCompleted { get; private set; }
    }
}
