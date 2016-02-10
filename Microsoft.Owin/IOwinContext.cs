using System.Collections.Generic;
using System.IO;
using Microsoft.Owin.Security;

namespace Microsoft.Owin
{
    public interface IOwinContext
    {
        T Get<T>(string key);

        IOwinContext Set<T>(string key, T value);

        IAuthenticationManager Authentication { get; }

        IDictionary<string, object> Environment { get; }

        IOwinRequest Request { get; }

        IOwinResponse Response { get; }

        TextWriter TraceOutput { get; set; }
    }
}
