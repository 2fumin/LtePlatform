namespace Microsoft.Owin
{
    public enum PipelineStage
    {
        Authenticate,
        PostAuthenticate,
        Authorize,
        PostAuthorize,
        ResolveCache,
        PostResolveCache,
        MapHandler,
        PostMapHandler,
        AcquireState,
        PostAcquireState,
        PreHandlerExecute
    }
}
