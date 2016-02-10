using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    public interface IOwinResponse
    {
        T Get<T>(string key);

        void OnSendingHeaders(Action<object> callback, object state);

        void Redirect(string location);

        IOwinResponse Set<T>(string key, T value);

        void Write(string text);

        void Write(byte[] data);

        void Write(byte[] data, int offset, int count);

        Task WriteAsync(string text);

        Task WriteAsync(byte[] data);

        Task WriteAsync(string text, CancellationToken token);

        Task WriteAsync(byte[] data, CancellationToken token);

        Task WriteAsync(byte[] data, int offset, int count, CancellationToken token);

        Stream Body { get; set; }

        long? ContentLength { get; set; }

        string ContentType { get; set; }

        IOwinContext Context { get; }

        ResponseCookieCollection Cookies { get; }

        IDictionary<string, object> Environment { get; }

        string ETag { get; set; }

        DateTimeOffset? Expires { get; set; }

        IHeaderDictionary Headers { get; }

        string Protocol { get; set; }

        string ReasonPhrase { get; set; }

        int StatusCode { get; set; }
    }
}
