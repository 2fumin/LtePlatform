using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Owin.Security.OAuth.Messages
{
    public class AuthorizeEndpointRequest
    {
        public AuthorizeEndpointRequest(IReadableStringCollection parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            Scope = new List<string>();
            foreach (var pair in parameters)
            {
                AddParameter(pair.Key, parameters.Get(pair.Key));
            }
        }

        private void AddParameter(string name, string value)
        {
            if (string.Equals(name, "response_type", StringComparison.Ordinal))
            {
                ResponseType = value;
            }
            else if (string.Equals(name, "client_id", StringComparison.Ordinal))
            {
                ClientId = value;
            }
            else if (string.Equals(name, "redirect_uri", StringComparison.Ordinal))
            {
                RedirectUri = value;
            }
            else if (string.Equals(name, "scope", StringComparison.Ordinal))
            {
                Scope = value.Split(' ');
            }
            else if (string.Equals(name, "state", StringComparison.Ordinal))
            {
                State = value;
            }
            else if (string.Equals(name, "response_mode", StringComparison.Ordinal))
            {
                ResponseMode = value;
            }
        }

        public bool ContainsGrantType(string responseType)
        {
            return ResponseType.Split(' ').Any(str => string.Equals(str, responseType, StringComparison.Ordinal));
        }

        public string ClientId { get; set; }

        public bool IsAuthorizationCodeGrantType => ContainsGrantType("code");

        public bool IsFormPostResponseMode => string.Equals(ResponseMode, "form_post", StringComparison.Ordinal);

        public bool IsImplicitGrantType => ContainsGrantType("token");

        public string RedirectUri { get; set; }

        public string ResponseMode { get; set; }

        public string ResponseType { get; set; }

        public IList<string> Scope { get; private set; }

        public string State { get; set; }
    }
}
