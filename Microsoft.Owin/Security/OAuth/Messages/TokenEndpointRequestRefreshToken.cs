using System.Collections.Generic;

namespace Microsoft.Owin.Security.OAuth.Messages
{
    public class TokenEndpointRequestRefreshToken
    {
        public string RefreshToken { get; set; }

        public IList<string> Scope { get; set; }
    }
}
