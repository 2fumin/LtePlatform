using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Builder
{
    internal class NotFound
    {
        private static readonly Task Completed = CreateCompletedTask();

        private static Task CreateCompletedTask()
        {
            TaskCompletionSource<object> source = new TaskCompletionSource<object>();
            source.SetResult(null);
            return source.Task;
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            env[OwinConstants.ResponseStatusCode] = 0x194;
            return Completed;
        }
    }
}
