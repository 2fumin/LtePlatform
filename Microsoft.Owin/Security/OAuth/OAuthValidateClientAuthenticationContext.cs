using System;
using System.Text;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthValidateClientAuthenticationContext : BaseValidatingClientContext
    {
        public OAuthValidateClientAuthenticationContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            IReadableStringCollection parameters) : base(context, options, null)
        {
            Parameters = parameters;
        }

        public bool TryGetBasicCredentials(out string clientId, out string clientSecret)
        {
            var str = Request.Headers.Get("Authorization");
            if (!string.IsNullOrWhiteSpace(str) && str.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var bytes = Convert.FromBase64String(str.Substring("Basic ".Length).Trim());
                    var str2 = Encoding.UTF8.GetString(bytes);
                    var index = str2.IndexOf(':');
                    if (index >= 0)
                    {
                        clientId = str2.Substring(0, index);
                        clientSecret = str2.Substring(index + 1);
                        ClientId = clientId;
                        return true;
                    }
                }
                catch (FormatException)
                {
                }
                catch (ArgumentException)
                {
                }
            }
            clientId = null;
            clientSecret = null;
            return false;
        }

        public bool TryGetFormCredentials(out string clientId, out string clientSecret)
        {
            clientId = Parameters.Get("client_id");
            if (!string.IsNullOrEmpty(clientId))
            {
                clientSecret = Parameters.Get("client_secret");
                ClientId = clientId;
                return true;
            }
            clientId = null;
            clientSecret = null;
            return false;
        }

        public bool Validated(string clientId)
        {
            ClientId = clientId;
            return Validated();
        }

        public IReadableStringCollection Parameters { get; }
    }
}
