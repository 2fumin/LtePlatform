namespace Microsoft.Owin.Host.SystemWeb.IntegratedPipeline
{
    internal class IntegratedPipelineBlueprint
    {
        public IntegratedPipelineBlueprint(OwinAppContext appContext, IntegratedPipelineBlueprintStage firstStage, string pathBase)
        {
            AppContext = appContext;
            FirstStage = firstStage;
            PathBase = pathBase;
        }

        public OwinAppContext AppContext { get; }

        public IntegratedPipelineBlueprintStage FirstStage { get; }

        public string PathBase { get; }
    }
}
