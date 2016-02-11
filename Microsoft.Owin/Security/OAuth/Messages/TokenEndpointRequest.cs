using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth.Messages
{
    using Owin;
    using System;
    using System.Runtime.CompilerServices;

    public class TokenEndpointRequest
    {
        public TokenEndpointRequest(IReadableStringCollection parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            Func<string, string> func = parameters.Get;
            Parameters = parameters;
            GrantType = func("grant_type");
            ClientId = func("client_id");
            if (string.Equals(GrantType, "authorization_code", StringComparison.Ordinal))
            {
                TokenEndpointRequestAuthorizationCode code = new TokenEndpointRequestAuthorizationCode
                {
                    Code = func("code"),
                    RedirectUri = func("redirect_uri")
                };
                AuthorizationCodeGrant = code;
            }
            else if (string.Equals(GrantType, "client_credentials", StringComparison.Ordinal))
            {
                TokenEndpointRequestClientCredentials credentials = new TokenEndpointRequestClientCredentials
                {
                    Scope = (func("scope") ?? string.Empty).Split(new char[] { ' ' })
                };
                ClientCredentialsGrant = credentials;
            }
            else if (string.Equals(GrantType, "refresh_token", StringComparison.Ordinal))
            {
                TokenEndpointRequestRefreshToken token = new TokenEndpointRequestRefreshToken
                {
                    RefreshToken = func("refresh_token"),
                    Scope = (func("scope") ?? string.Empty).Split(new char[] { ' ' })
                };
                RefreshTokenGrant = token;
            }
            else if (string.Equals(GrantType, "password", StringComparison.Ordinal))
            {
                TokenEndpointRequestResourceOwnerPasswordCredentials credentials2 = new TokenEndpointRequestResourceOwnerPasswordCredentials
                {
                    UserName = func("username"),
                    Password = func("password"),
                    Scope = (func("scope") ?? string.Empty).Split(new char[] { ' ' })
                };
                ResourceOwnerPasswordCredentialsGrant = credentials2;
            }
            else if (!string.IsNullOrEmpty(GrantType))
            {
                TokenEndpointRequestCustomExtension extension = new TokenEndpointRequestCustomExtension
                {
                    Parameters = parameters
                };
                CustomExtensionGrant = extension;
            }
        }

        public TokenEndpointRequestAuthorizationCode AuthorizationCodeGrant { get; }

        public TokenEndpointRequestClientCredentials ClientCredentialsGrant { get; }

        public string ClientId { get; private set; }

        public TokenEndpointRequestCustomExtension CustomExtensionGrant { get; }

        public string GrantType { get; }

        public bool IsAuthorizationCodeGrantType
        {
            get
            {
                return (AuthorizationCodeGrant != null);
            }
        }

        public bool IsClientCredentialsGrantType
        {
            get
            {
                return (ClientCredentialsGrant != null);
            }
        }

        public bool IsCustomExtensionGrantType
        {
            get
            {
                return (CustomExtensionGrant != null);
            }
        }

        public bool IsRefreshTokenGrantType
        {
            get
            {
                return (RefreshTokenGrant != null);
            }
        }

        public bool IsResourceOwnerPasswordCredentialsGrantType
        {
            get
            {
                return (ResourceOwnerPasswordCredentialsGrant != null);
            }
        }

        public IReadableStringCollection Parameters { get; private set; }

        public TokenEndpointRequestRefreshToken RefreshTokenGrant { get; }

        public TokenEndpointRequestResourceOwnerPasswordCredentials ResourceOwnerPasswordCredentialsGrant { get; }
    }
}
