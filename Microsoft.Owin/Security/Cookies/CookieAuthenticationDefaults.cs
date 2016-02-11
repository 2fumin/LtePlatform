namespace Microsoft.Owin.Security.Cookies
{
    public static class CookieAuthenticationDefaults
    {
        public const string AuthenticationType = "Cookies";
        public const string CookiePrefix = ".AspNet.";
        public static readonly PathString LoginPath = new PathString("/Account/Login");
        public static readonly PathString LogoutPath = new PathString("/Account/Logout");
        public const string ReturnUrlParameter = "ReturnUrl";
    }
}
