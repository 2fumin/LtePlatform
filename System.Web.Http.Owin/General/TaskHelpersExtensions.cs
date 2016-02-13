namespace System.Threading.Tasks
{
    internal static class TaskHelpersExtensions
    {
        internal static async Task<object> CastToObject(this Task task)
        {
            await task;
            return null;
        }

        internal static async Task<object> CastToObject<T>(this Task<T> task)
        {
            return await task;
        }

        internal static void ThrowIfFaulted(this Task task)
        {
            task.GetAwaiter().GetResult();
        }

        internal static bool TryGetResult<TResult>(this Task<TResult> task, out TResult result)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                result = task.Result;
                return true;
            }
            result = default(TResult);
            return false;
        }
    }
}
