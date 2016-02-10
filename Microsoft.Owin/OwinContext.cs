using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Owin.Security;

namespace Microsoft.Owin
{
    public class OwinContext : IOwinContext
    {
        public OwinContext()
        {
            IDictionary<string, object> environment = new Dictionary<string, object>(StringComparer.Ordinal);
            environment[OwinConstants.RequestHeaders] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            environment[OwinConstants.RequestHeaders] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            Environment = environment;
            Request = new OwinRequest(environment);
            Response = new OwinResponse(environment);
        }

        public OwinContext(IDictionary<string, object> environment)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }
            Environment = environment;
            Request = new OwinRequest(environment);
            Response = new OwinResponse(environment);
        }

        public virtual T Get<T>(string key)
        {
            object obj2;
            if (!Environment.TryGetValue(key, out obj2))
            {
                return default(T);
            }
            return (T)obj2;
        }

        public virtual IOwinContext Set<T>(string key, T value)
        {
            Environment[key] = value;
            return this;
        }

        public IAuthenticationManager Authentication => new AuthenticationManager(this);

        public virtual IDictionary<string, object> Environment { get; }

        public virtual IOwinRequest Request { get; }

        public virtual IOwinResponse Response { get; }

        public virtual TextWriter TraceOutput
        {
            get
            {
                return Get<TextWriter>("host.TraceOutput");
            }
            set
            {
                Set<TextWriter>("host.TraceOutput", value);
            }
        }
    }
}
