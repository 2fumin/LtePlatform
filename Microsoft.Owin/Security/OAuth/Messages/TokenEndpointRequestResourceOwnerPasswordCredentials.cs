using System.Collections.Generic;

namespace Microsoft.Owin.Security.OAuth.Messages
{
    public class TokenEndpointRequestResourceOwnerPasswordCredentials
    {
        public string Password { get; set; }

        public IList<string> Scope { get; set; }

        public string UserName { get; set; }
    }
}
