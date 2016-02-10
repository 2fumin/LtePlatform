namespace Microsoft.Owin.Infrastructure
{
    public interface ICookieManager
    {
        void AppendResponseCookie(IOwinContext context, string key, string value, CookieOptions options);

        void DeleteCookie(IOwinContext context, string key, CookieOptions options);

        string GetRequestCookie(IOwinContext context, string key);
    }
}
