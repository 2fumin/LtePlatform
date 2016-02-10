using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin
{
    public class OwinResponse : IOwinResponse
    {
        public OwinResponse()
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
            dictionary[OwinConstants.RequestHeaders] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            dictionary[OwinConstants.ResponseHeaders] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            Environment = dictionary;
        }

        public OwinResponse(IDictionary<string, object> environment)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }
            Environment = environment;
        }

        public virtual T Get<T>(string key)
        {
            return Get(key, default(T));
        }

        private T Get<T>(string key, T fallback)
        {
            object obj2;
            if (!Environment.TryGetValue(key, out obj2))
            {
                return fallback;
            }
            return (T)obj2;
        }

        public virtual void OnSendingHeaders(Action<object> callback, object state)
        {
            var action = Get<Action<Action<object>, object>>(OwinConstants.CommonKeys.OnSendingHeaders);
            if (action == null)
            {
                throw new NotSupportedException(Resources.Exception_MissingOnSendingHeaders);
            }
            action(callback, state);
        }

        public virtual void Redirect(string location)
        {
            StatusCode = 0x12e;
            OwinHelpers.SetHeader(RawHeaders, "Location", location);
        }

        public virtual IOwinResponse Set<T>(string key, T value)
        {
            Environment[key] = value;
            return this;
        }

        public virtual void Write(string text)
        {
            Write(Encoding.UTF8.GetBytes(text));
        }

        public virtual void Write(byte[] data)
        {
            Write(data, 0, data?.Length ?? 0);
        }

        public virtual void Write(byte[] data, int offset, int count)
        {
            Body.Write(data, offset, count);
        }

        public virtual Task WriteAsync(string text)
        {
            return WriteAsync(text, CancellationToken.None);
        }

        public virtual Task WriteAsync(byte[] data)
        {
            return WriteAsync(data, CancellationToken.None);
        }

        public virtual Task WriteAsync(string text, CancellationToken token)
        {
            return WriteAsync(Encoding.UTF8.GetBytes(text), token);
        }

        public virtual Task WriteAsync(byte[] data, CancellationToken token)
        {
            return WriteAsync(data, 0, data?.Length ?? 0, token);
        }

        public virtual Task WriteAsync(byte[] data, int offset, int count, CancellationToken token)
        {
            return Body.WriteAsync(data, offset, count, token);
        }

        public virtual Stream Body
        {
            get
            {
                return Get<Stream>(OwinConstants.ResponseBody);
            }
            set
            {
                Set(OwinConstants.ResponseBody, value);
            }
        }

        public virtual long? ContentLength
        {
            get
            {
                long num;
                return long.TryParse(OwinHelpers.GetHeader(RawHeaders, "Content-Length"), out num) ? new long?(num) : null;
            }
            set
            {
                if (value.HasValue)
                {
                    OwinHelpers.SetHeader(RawHeaders, "Content-Length", value.Value.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    RawHeaders.Remove("Content-Length");
                }
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

        public virtual ResponseCookieCollection Cookies => new ResponseCookieCollection(Headers);

        public IDictionary<string, object> Environment { get; }

        public virtual string ETag
        {
            get
            {
                return OwinHelpers.GetHeader(RawHeaders, "ETag");
            }
            set
            {
                OwinHelpers.SetHeader(RawHeaders, "ETag", value);
            }
        }

        public virtual DateTimeOffset? Expires
        {
            get
            {
                DateTimeOffset offset;
                if (DateTimeOffset.TryParse(OwinHelpers.GetHeader(RawHeaders, "Expires"), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out offset))
                {
                    return offset;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    OwinHelpers.SetHeader(RawHeaders, "Expires", value.Value.ToString("r", CultureInfo.InvariantCulture));
                }
                else
                {
                    RawHeaders.Remove("Expires");
                }
            }
        }

        public virtual IHeaderDictionary Headers => new HeaderDictionary(RawHeaders);

        public virtual string Protocol
        {
            get
            {
                return Get<string>(OwinConstants.ResponseProtocol);
            }
            set
            {
                Set(OwinConstants.ResponseProtocol, value);
            }
        }

        private IDictionary<string, string[]> RawHeaders => Get<IDictionary<string, string[]>>(OwinConstants.ResponseHeaders);

        public virtual string ReasonPhrase
        {
            get
            {
                return Get<string>(OwinConstants.ResponseReasonPhrase);
            }
            set
            {
                Set(OwinConstants.ResponseReasonPhrase, value);
            }
        }

        public virtual int StatusCode
        {
            get
            {
                return Get(OwinConstants.ResponseStatusCode, 200);
            }
            set
            {
                Set(OwinConstants.ResponseStatusCode, value);
            }
        }
    }
}
