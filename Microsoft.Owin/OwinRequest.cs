using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin
{
    public class OwinRequest : IOwinRequest
    {
        public OwinRequest()
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
            dictionary["owin.RequestHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            dictionary["owin.ResponseHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            Environment = dictionary;
        }

        public OwinRequest(IDictionary<string, object> environment)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }
            Environment = environment;
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

        public async Task<IFormCollection> ReadFormAsync()
        {
            var form = Get<IFormCollection>("Microsoft.Owin.Form#collection");
            if (form != null) return form;
            string text;
            using (var reader = new StreamReader(Body, Encoding.UTF8, true, 0x1000, true))
            {
                text = await reader.ReadToEndAsync();
            }
            form = OwinHelpers.GetForm(text);
            Set("Microsoft.Owin.Form#collection", form);
            return form;
        }

        public virtual IOwinRequest Set<T>(string key, T value)
        {
            Environment[key] = value;
            return this;
        }

        public virtual string Accept
        {
            get
            {
                return OwinHelpers.GetHeader(RawHeaders, "Accept");
            }
            set
            {
                OwinHelpers.SetHeader(RawHeaders, "Accept", value);
            }
        }

        public virtual Stream Body
        {
            get
            {
                return Get<Stream>("owin.RequestBody");
            }
            set
            {
                Set("owin.RequestBody", value);
            }
        }

        public virtual string CacheControl
        {
            get
            {
                return OwinHelpers.GetHeader(RawHeaders, "Cache-Control");
            }
            set
            {
                OwinHelpers.SetHeader(RawHeaders, "Cache-Control", value);
            }
        }

        public virtual CancellationToken CallCancelled
        {
            get
            {
                return Get<CancellationToken>("owin.CallCancelled");
            }
            set
            {
                Set("owin.CallCancelled", value);
            }
        }

        public virtual string ContentType
        {
            get
            {
                return OwinHelpers.GetHeader(RawHeaders, "Content-Type");
            }
            set
            {
                OwinHelpers.SetHeader(RawHeaders, "Content-Type", value);
            }
        }

        public virtual IOwinContext Context => new OwinContext(Environment);

        public RequestCookieCollection Cookies => new RequestCookieCollection(OwinHelpers.GetCookies(this));

        public IDictionary<string, object> Environment { get; }

        public virtual IHeaderDictionary Headers => new HeaderDictionary(RawHeaders);

        public virtual HostString Host
        {
            get
            {
                return new HostString(OwinHelpers.GetHost(this));
            }
            set
            {
                OwinHelpers.SetHeader(RawHeaders, "Host", value.Value);
            }
        }

        public virtual bool IsSecure => string.Equals(Scheme, "HTTPS", StringComparison.OrdinalIgnoreCase);

        public virtual string LocalIpAddress
        {
            get
            {
                return Get<string>("server.LocalIpAddress");
            }
            set
            {
                Set("server.LocalIpAddress", value);
            }
        }

        public virtual int? LocalPort
        {
            get
            {
                int num;
                return int.TryParse(LocalPortString, out num) ? new int?(num) : null;
            }
            set
            {
                if (value.HasValue)
                {
                    LocalPortString = value.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    Environment.Remove("server.LocalPort");
                }
            }
        }

        private string LocalPortString
        {
            get
            {
                return Get<string>("server.LocalPort");
            }
            set
            {
                Set("server.LocalPort", value);
            }
        }

        public virtual string MediaType
        {
            get
            {
                return OwinHelpers.GetHeader(RawHeaders, "Media-Type");
            }
            set
            {
                OwinHelpers.SetHeader(RawHeaders, "Media-Type", value);
            }
        }

        public virtual string Method
        {
            get
            {
                return Get<string>("owin.RequestMethod");
            }
            set
            {
                Set("owin.RequestMethod", value);
            }
        }

        public virtual PathString Path
        {
            get
            {
                return new PathString(Get<string>("owin.RequestPath"));
            }
            set
            {
                Set("owin.RequestPath", value.Value);
            }
        }

        public virtual PathString PathBase
        {
            get
            {
                return new PathString(Get<string>("owin.RequestPathBase"));
            }
            set
            {
                Set("owin.RequestPathBase", value.Value);
            }
        }

        public virtual string Protocol
        {
            get
            {
                return Get<string>("owin.RequestProtocol");
            }
            set
            {
                Set("owin.RequestProtocol", value);
            }
        }

        public virtual IReadableStringCollection Query => new ReadableStringCollection(OwinHelpers.GetQuery(this));

        public virtual QueryString QueryString
        {
            get
            {
                return new QueryString(Get<string>("owin.RequestQueryString"));
            }
            set
            {
                Set("owin.RequestQueryString", value.Value);
            }
        }

        private IDictionary<string, string[]> RawHeaders => Get<IDictionary<string, string[]>>("owin.RequestHeaders");

        public virtual string RemoteIpAddress
        {
            get
            {
                return Get<string>("server.RemoteIpAddress");
            }
            set
            {
                Set("server.RemoteIpAddress", value);
            }
        }

        public virtual int? RemotePort
        {
            get
            {
                int num;
                return int.TryParse(RemotePortString, out num) ? new int?(num) : null;
            }
            set
            {
                if (value.HasValue)
                {
                    RemotePortString = value.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    Environment.Remove("server.RemotePort");
                }
            }
        }

        private string RemotePortString
        {
            get
            {
                return Get<string>("server.RemotePort");
            }
            set
            {
                Set("server.RemotePort", value);
            }
        }

        public virtual string Scheme
        {
            get
            {
                return Get<string>("owin.RequestScheme");
            }
            set
            {
                Set("owin.RequestScheme", value);
            }
        }

        public virtual Uri Uri => new Uri(string.Concat(Scheme, Uri.SchemeDelimiter, Host, PathBase, Path, QueryString));

        public virtual IPrincipal User
        {
            get
            {
                return Get<IPrincipal>("server.User");
            }
            set
            {
                Set("server.User", value);
            }
        }
        
    }
}
