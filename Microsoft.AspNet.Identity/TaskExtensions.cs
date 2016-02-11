using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    internal static class TaskExtensions
    {
        public static CultureAwaiter WithCurrentCulture(this Task task)
        {
            return new CultureAwaiter(task);
        }

        public static CultureAwaiter<T> WithCurrentCulture<T>(this Task<T> task)
        {
            return new CultureAwaiter<T>(task);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CultureAwaiter : ICriticalNotifyCompletion
        {
            private readonly Task _task;
            public CultureAwaiter(Task task)
            {
                _task = task;
            }

            public CultureAwaiter GetAwaiter()
            {
                return this;
            }

            public bool IsCompleted => _task.IsCompleted;

            public void GetResult()
            {
                _task.GetAwaiter().GetResult();
            }

            public void OnCompleted(Action continuation)
            {
                throw new NotImplementedException();
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                _task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(delegate {
                    CultureInfo info1 = Thread.CurrentThread.CurrentCulture;
                    CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                    Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                    try
                    {
                        continuation();
                    }
                    finally
                    {
                        Thread.CurrentThread.CurrentCulture = info1;
                        Thread.CurrentThread.CurrentUICulture = currentUICulture;
                    }
                });
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CultureAwaiter<T> : ICriticalNotifyCompletion
        {
            private readonly Task<T> _task;
            public CultureAwaiter(Task<T> task)
            {
                _task = task;
            }

            public CultureAwaiter<T> GetAwaiter()
            {
                return this;
            }

            public bool IsCompleted => _task.IsCompleted;

            public T GetResult()
            {
                return _task.GetAwaiter().GetResult();
            }

            public void OnCompleted(Action continuation)
            {
                throw new NotImplementedException();
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                _task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(delegate {
                    CultureInfo info1 = Thread.CurrentThread.CurrentCulture;
                    CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                    Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                    try
                    {
                        continuation();
                    }
                    finally
                    {
                        Thread.CurrentThread.CurrentCulture = info1;
                        Thread.CurrentThread.CurrentUICulture = currentUICulture;
                    }
                });
            }
        }
    }
}
