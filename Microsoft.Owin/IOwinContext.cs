using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    using Microsoft.Owin.Security;
    using System;
    using System.Collections.Generic;
    using System.IO;

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
