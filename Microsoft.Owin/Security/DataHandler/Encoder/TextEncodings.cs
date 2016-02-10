namespace Microsoft.Owin.Security.DataHandler.Encoder
{
    public static class TextEncodings
    {
        public static ITextEncoder Base64 { get; } = new Base64TextEncoder();

        public static ITextEncoder Base64Url { get; } = new Base64UrlTextEncoder();
    }
}
