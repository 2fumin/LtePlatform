using System.Web.Http.ExceptionHandling;

namespace System.Web.Http.Owin
{
    public static class OwinExceptionCatchBlocks
    {
        public static ExceptionContextCatchBlock HttpMessageHandlerAdapterBufferContent { get; } 
            = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".BufferContent", true, true);

        public static ExceptionContextCatchBlock HttpMessageHandlerAdapterBufferError { get; } 
            = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".BufferError", true, false);

        public static ExceptionContextCatchBlock HttpMessageHandlerAdapterComputeContentLength { get; } 
            = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".ComputeContentLength", true, false);

        public static ExceptionContextCatchBlock HttpMessageHandlerAdapterStreamContent { get; } 
            = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".StreamContent", true, false);
    }
}
