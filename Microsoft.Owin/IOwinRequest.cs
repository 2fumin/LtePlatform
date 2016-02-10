using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    public interface IOwinRequest
    {
        T Get<T>(string key);

        Task<IFormCollection> ReadFormAsync();

        IOwinRequest Set<T>(string key, T value);

        string Accept { get; set; }

        Stream Body { get; set; }

        string CacheControl { get; set; }

        CancellationToken CallCancelled { get; set; }

        string ContentType { get; set; }

        IOwinContext Context { get; }

        RequestCookieCollection Cookies { get; }

        IDictionary<string, object> Environment { get; }

        IHeaderDictionary Headers { get; }

        HostString Host { get; set; }

        bool IsSecure { get; }

        string LocalIpAddress { get; set; }

        int? LocalPort { get; set; }

        string MediaType { get; set; }

        string Method { get; set; }

        PathString Path { get; set; }

        PathString PathBase { get; set; }

        string Protocol { get; set; }

        IReadableStringCollection Query { get; }

        Owin.QueryString QueryString { get; set; }

        string RemoteIpAddress { get; set; }

        int? RemotePort { get; set; }

        string Scheme { get; set; }

        Uri Uri { get; }

        IPrincipal User { get; set; }
    }
}
