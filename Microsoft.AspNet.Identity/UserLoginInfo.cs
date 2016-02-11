namespace Microsoft.AspNet.Identity
{
    public sealed class UserLoginInfo
    {
        public UserLoginInfo(string loginProvider, string providerKey)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
