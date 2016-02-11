using System.Collections.Generic;

namespace Microsoft.Owin.Security.OAuth.Messages
{
    public class TokenEndpointRequestClientCredentials
    {
        public IList<string> Scope { get; set; }
    }
}
