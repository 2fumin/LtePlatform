using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Microsoft.Owin.Host.SystemWeb.IntegratedPipeline
{
    internal class IntegratedPipelineContextStage
    {
        private readonly IntegratedPipelineContext _context;
        private bool _responseShouldEnd;
        private StageAsyncResult _result;
        private readonly IntegratedPipelineBlueprintStage _stage;

        public IntegratedPipelineContextStage(IntegratedPipelineContext context, IntegratedPipelineBlueprintStage stage)
        {
            _context = context;
            _stage = stage;
        }

        public async Task<IAsyncResult> BeginEvent(object sender, EventArgs e, AsyncCallback cb, object extradata)
        {
            if (_result != null)
            {
                throw new InvalidOperationException();
            }
            if (_context.PreventNextStage)
            {
                var result = new StageAsyncResult(cb, extradata, delegate {
                });
                result.TryComplete();
                result.InitialThreadReturning();
                return result;
            }
            _context.PreventNextStage = true;
            _responseShouldEnd = true;
            _context.PushExecutingStage(this);
            var entryPoint = _stage.EntryPoint ??
                             _context.PrepareInitialContext(
                                 (HttpApplication) sender);
            var environment = _context.TakeLastEnvironment();
            var tcs = _context.TakeLastCompletionSource();
            var result2 = new StageAsyncResult(cb, extradata, delegate
            {
                var application1 = (HttpApplication) sender;
                if (_responseShouldEnd)
                {
                    application1.CompleteRequest();
                }
            });
            _result = result2;
            environment[Constants.IntegratedPipelineCurrentStage] = _stage.Name;
            await RunApp(entryPoint, environment, tcs, result2);
            result2.InitialThreadReturning();
            return result2;
        }

        public Task DefaultAppInvoked(IDictionary<string, object> env)
        {
            return Epilog(env);
        }

        public void EndEvent(IAsyncResult ar)
        {
            StageAsyncResult.End(ar);
        }

        private Task Epilog(IDictionary<string, object> env)
        {
            var completionSource = new TaskCompletionSource<object>();
            _responseShouldEnd = false;
            _context.PushLastObjects(env, completionSource);
            var result = Interlocked.Exchange(ref _result, null);
            result?.TryComplete();
            return completionSource.Task;
        }

        public Task ExitPointInvoked(IDictionary<string, object> env)
        {
            _context.PreventNextStage = false;
            return Epilog(env);
        }

        public void Reset()
        {
            _result = null;
            _responseShouldEnd = false;
        }

        private async Task RunApp(Func<IDictionary<string, object>, Task> entryPoint,
            IDictionary<string, object> environment, TaskCompletionSource<object> tcs, StageAsyncResult result)
        {
            try
            {
                await entryPoint(environment);
                tcs.TrySetResult(null);
                result.TryComplete();
            }
            catch (Exception exception)
            {
                tcs.TrySetException(exception);
                result.TryComplete();
            }
        }

    }
}
