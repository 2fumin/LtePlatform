using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Owin.Infrastructure
{
    public static class WebUtilities
    {
        public static string AddQueryString(string uri, IDictionary<string, string> queryString)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (queryString == null)
            {
                throw new ArgumentNullException(nameof(queryString));
            }
            var builder = new StringBuilder();
            builder.Append(uri);
            var flag = uri.IndexOf('?') != -1;
            foreach (var pair in queryString)
            {
                builder.Append(flag ? '&' : '?');
                builder.Append(Uri.EscapeDataString(pair.Key));
                builder.Append('=');
                builder.Append(Uri.EscapeDataString(pair.Value));
                flag = true;
            }
            return builder.ToString();
        }

        public static string AddQueryString(string uri, string queryString)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (string.IsNullOrEmpty(queryString))
            {
                return uri;
            }
            var flag = uri.IndexOf('?') != -1;
            return (uri + (flag ? "&" : "?") + queryString);
        }

        public static string AddQueryString(string uri, string name, string value)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var flag = uri.IndexOf('?') != -1;
            return (uri + (flag ? "&" : "?") + Uri.EscapeDataString(name) + "=" + Uri.EscapeDataString(value));
        }
    }
}
