using System;

namespace Microsoft.Owin.Extensions
{
    public static class IntegratedPipelineExtensions
    {
        private const string IntegratedPipelineStageMarker = "integratedpipeline.StageMarker";

        public static IAppBuilder UseStageMarker(this IAppBuilder app, PipelineStage stage)
        {
            return app.UseStageMarker(stage.ToString());
        }

        public static IAppBuilder UseStageMarker(this IAppBuilder app, string stageName)
        {
            object obj2;
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (app.Properties.TryGetValue("integratedpipeline.StageMarker", out obj2))
            {
                Action<IAppBuilder, string> action = (Action<IAppBuilder, string>)obj2;
                action(app, stageName);
            }
            return app;
        }
    }
}
