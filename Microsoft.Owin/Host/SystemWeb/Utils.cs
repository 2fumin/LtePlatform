using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal static class Utils
    {
        internal static readonly Task CancelledTask = CreateCancelledTask();
        internal static readonly Task CompletedTask = CreateCompletedTask();

        private static Task CreateCancelledTask()
        {
            var source = new TaskCompletionSource<object>();
            source.TrySetCanceled();
            return source.Task;
        }

        private static Task CreateCompletedTask()
        {
            var source = new TaskCompletionSource<object>();
            source.TrySetResult(null);
            return source.Task;
        }

        internal static Task CreateFaultedTask(Exception ex)
        {
            var source = new TaskCompletionSource<object>();
            source.TrySetException(ex);
            return source.Task;
        }

        internal static string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return (path ?? string.Empty);
            }
            if (path.Length == 1)
            {
                if (path[0] != '/')
                {
                    return ('/' + path);
                }
                return string.Empty;
            }
            if (path[0] != '/')
            {
                return ('/' + path);
            }
            return path;
        }
    }
}
