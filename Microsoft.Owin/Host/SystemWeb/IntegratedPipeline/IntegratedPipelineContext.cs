using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb.IntegratedPipeline
{
    internal class IntegratedPipelineContext
    {
        private readonly IntegratedPipelineBlueprint _blueprint;
        private State _state;
        private static readonly IList<string> StageNames = new[]
        {
            "Authenticate",
            "PostAuthenticate",
            "Authorize",
            "PostAuthorize",
            "ResolveCache",
            "PostResolveCache",
            "MapHandler",
            "PostMapHandler",
            "AcquireState",
            "PostAcquireState",
            "PreHandlerExecute"
        };

        public IntegratedPipelineContext(IntegratedPipelineBlueprint blueprint)
        {
            _blueprint = blueprint;
        }

        private async Task<IAsyncResult> BeginFinalWork(object sender, EventArgs e, AsyncCallback cb, object extradata)
        {
            var result = new StageAsyncResult(cb, extradata, delegate {
            });
            var source = TakeLastCompletionSource();
            source?.TrySetResult(null);
            if (_state.OriginalTask != null)
            {
                await DoFinalWork(result);
            }
            else
            {
                result.TryComplete();
            }
            result.InitialThreadReturning();
            return result;
        }

        public static Task DefaultAppInvoked(IDictionary<string, object> env)
        {
            object obj2;
            if (!env.TryGetValue(Constants.IntegratedPipelineContext, out obj2))
            {
                throw new InvalidOperationException();
            }
            var context = (IntegratedPipelineContext)obj2;
            return context._state.ExecutingStage.DefaultAppInvoked(env);
        }

        private async Task DoFinalWork(StageAsyncResult result)
        {
            try
            {
                await _state.OriginalTask;
                _state.CallContext.OnEnd();
                CallContextAsyncResult.End(_state.CallContext.AsyncResult);
                result.TryComplete();
            }
            catch (Exception exception)
            {
                _state.CallContext.AbortIfHeaderSent();
                result.Fail(ErrorState.Capture(exception));
            }
        }

        private void EndFinalWork(IAsyncResult ar)
        {
            Reset();
            StageAsyncResult.End(ar);
        }

        public static Task ExitPointInvoked(IDictionary<string, object> env)
        {
            object obj2;
            if (!env.TryGetValue(Constants.IntegratedPipelineContext, out obj2))
            {
                throw new InvalidOperationException();
            }
            var context = (IntegratedPipelineContext)obj2;
            return context._state.ExecutingStage.ExitPointInvoked(env);
        }

        public IDictionary<string, object> GetInitialEnvironment(HttpApplication application)
        {
            if (_state.CallContext != null)
            {
                return _state.CallContext.Environment;
            }
            var requestPathBase = application.Request.Path.Substring(0, _blueprint.PathBase.Length);
            if (application.Request.AppRelativeCurrentExecutionFilePath != null)
            {
                var requestPath = application.Request.AppRelativeCurrentExecutionFilePath.Substring(1) + application.Request.PathInfo;
                _state.CallContext = _blueprint.AppContext.CreateCallContext(application.Request.RequestContext, requestPathBase, requestPath, null, null);
            }
            if (_state.CallContext == null) return null;
            _state.CallContext.CreateEnvironment();
            var environment = _state.CallContext.Environment;
            environment.IntegratedPipelineContext = this;
            return environment;
        }

        public void Initialize(HttpApplication application)
        {
            for (var stage = _blueprint.FirstStage; stage != null; stage = stage.NextStage)
            {
                var stage2 = new IntegratedPipelineContextStage(this, stage);
                switch (stage.Name)
                {
                    case "Authenticate":
                        application.AddOnAuthenticateRequestAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "PostAuthenticate":
                        application.AddOnPostAuthenticateRequestAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "Authorize":
                        application.AddOnAuthorizeRequestAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "PostAuthorize":
                        application.AddOnPostAuthorizeRequestAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "ResolveCache":
                        application.AddOnResolveRequestCacheAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "PostResolveCache":
                        application.AddOnPostResolveRequestCacheAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "MapHandler":
                        application.AddOnMapRequestHandlerAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "PostMapHandler":
                        application.AddOnPostMapRequestHandlerAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "AcquireState":
                        application.AddOnAcquireRequestStateAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "PostAcquireState":
                        application.AddOnPostAcquireRequestStateAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    case "PreHandlerExecute":
                        application.AddOnPreRequestHandlerExecuteAsync(stage2.BeginEvent, stage2.EndEvent);
                        break;

                    default:
                        throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, Resources.Exception_UnsupportedPipelineStage, stage.Name));
                }
            }
            application.AddOnEndRequestAsync(BeginFinalWork, EndFinalWork);
        }

        public Func<IDictionary<string, object>, Task> PrepareInitialContext(HttpApplication application)
        {
            var initialEnvironment = GetInitialEnvironment(application);
            var completionSource = new TaskCompletionSource<object>();
            _state.OriginalTask = completionSource.Task;
            PushLastObjects(initialEnvironment, completionSource);
            return _blueprint.AppContext.AppFunc;
        }

        public void PushExecutingStage(IntegratedPipelineContextStage stage)
        {
            var stage2 = Interlocked.Exchange(ref _state.ExecutingStage, stage);
            stage2?.Reset();
        }

        public void PushLastObjects(IDictionary<string, object> environment, TaskCompletionSource<object> completionSource)
        {
            var dictionary = Interlocked.CompareExchange(ref _state.LastEnvironment, environment, null);
            var source = Interlocked.CompareExchange(ref _state.LastCompletionSource, completionSource, null);
            if ((dictionary != null) || (source != null))
            {
                throw new InvalidOperationException();
            }
        }

        private void Reset()
        {
            PushExecutingStage(null);
            _state = new State();
        }

        public TaskCompletionSource<object> TakeLastCompletionSource()
        {
            return Interlocked.Exchange(ref _state.LastCompletionSource, null);
        }

        public IDictionary<string, object> TakeLastEnvironment()
        {
            return Interlocked.Exchange(ref _state.LastEnvironment, null);
        }

        internal static bool VerifyStageOrder(string stage1, string stage2)
        {
            var index = StageNames.IndexOf(stage1);
            var num2 = StageNames.IndexOf(stage2);
            return (((index != -1) && (num2 != -1)) && (index < num2));
        }

        public bool PreventNextStage
        {
            get
            {
                return _state.PreventNextStage;
            }
            set
            {
                _state.PreventNextStage = value;
            }
        }
        
    [StructLayout(LayoutKind.Sequential)]
    private struct State
    {
        public IDictionary<string, object> LastEnvironment;
        public TaskCompletionSource<object> LastCompletionSource;
        public Task OriginalTask;
        public OwinCallContext CallContext;
        public bool PreventNextStage;
        public IntegratedPipelineContextStage ExecutingStage;
    }
}
}
