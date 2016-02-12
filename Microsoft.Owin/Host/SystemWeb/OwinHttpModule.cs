using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Host.SystemWeb.IntegratedPipeline;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal sealed class OwinHttpModule : IHttpModule
    {
        private static IntegratedPipelineBlueprint _blueprint;
        private static bool _blueprintInitialized;
        private static object _blueprintLock = new object();

        public void Dispose()
        {
        }

        private static void EnableIntegratedPipeline(IAppBuilder app, Action<IntegratedPipelineBlueprintStage> onStageCreated)
        {
            var stage = new IntegratedPipelineBlueprintStage
            {
                Name = "PreHandlerExecute"
            };
            onStageCreated(stage);
            Action<IAppBuilder, string> action = delegate (IAppBuilder builder, string name) {
                Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>> middleware 
                = delegate (Func<IDictionary<string, object>, Task> next) {
                    if (string.Equals(name, stage.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        return next;
                    }
                    if (!IntegratedPipelineContext.VerifyStageOrder(name, stage.Name))
                    {
                        return next;
                    }
                    stage.EntryPoint = next;
                    stage = new IntegratedPipelineBlueprintStage
                    {
                        Name = name,
                        NextStage = stage
                    };
                    onStageCreated(stage);
                    return IntegratedPipelineContext.ExitPointInvoked;
                };
                app.Use(middleware);
            };
            app.Properties[Constants.IntegratedPipelineStageMarker] = action;
            app.Properties[Constants.BuilderDefaultApp] =
                new Func<IDictionary<string, object>, Task>(IntegratedPipelineContext.DefaultAppInvoked);
        }

        public void Init(HttpApplication context)
        {
            var blueprint = LazyInitializer.EnsureInitialized(ref _blueprint, ref _blueprintInitialized,
                ref _blueprintLock, InitializeBlueprint);
            if (blueprint != null)
            {
                new IntegratedPipelineContext(blueprint).Initialize(context);
            }
        }

        private static IntegratedPipelineBlueprint InitializeBlueprint()
        {
            IntegratedPipelineBlueprintStage firstStage = null;
            var startup = OwinBuilder.GetAppStartup();
            var appContext = OwinBuilder.Build(delegate (IAppBuilder builder) {
                EnableIntegratedPipeline(builder, stage => firstStage = stage);
                startup(builder);
            });
            return new IntegratedPipelineBlueprint(appContext, firstStage, 
                Utils.NormalizePath(HttpRuntime.AppDomainAppVirtualPath));
        }
    }
}
