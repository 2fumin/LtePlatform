using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Owin.Infrastructure
{
    internal static class OwinHelpers
    {
        private static readonly Action<string, string, object> AddCookieCallback;
        private static readonly char[] AmpersandAndSemicolon;
        private static readonly Action<string, string, object> AppendItemCallback;
        private static readonly char[] SemicolonAndComma;

        static OwinHelpers()
        {
            AddCookieCallback = delegate(string name, string value, object state)
            {
                var dictionary = (IDictionary<string, string>) state;
                if (!dictionary.ContainsKey(name))
                {
                    dictionary.Add(name, value);
                }
            };
            SemicolonAndComma = new[] {';', ','};
            AppendItemCallback = delegate(string name, string value, object state)
            {
                List<string> list;
                var dictionary = (IDictionary<string, List<string>>) state;
                if (!dictionary.TryGetValue(name, out list))
                {
                    dictionary.Add(name, new List<string>(1) {value});
                }
                else
                {
                    list.Add(value);
                }
            };
            AmpersandAndSemicolon = new[] {'&', ';'};
        }

        public static void AppendHeader(IDictionary<string, string[]> headers, string key, string values)
        {
            if (string.IsNullOrWhiteSpace(values)) return;
            var header = GetHeader(headers, key);
            if (header == null)
            {
                SetHeader(headers, key, values);
            }
            else
            {
                headers[key] = new[] {header + "," + values};
            }
        }

        public static void AppendHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if ((values != null) && (values.Length != 0))
            {
                var header = GetHeader(headers, key);
                if (header == null)
                {
                    SetHeaderJoined(headers, key, values);
                }
                else
                {
                    headers[key] = new[]
                    {header + "," + string.Join(",", from value in values select QuoteIfNeeded(value))};
                }
            }
        }

        public static void AppendHeaderUnmodified(IDictionary<string, string[]> headers, string key,
            params string[] values)
        {
            if ((values != null) && (values.Length != 0))
            {
                var headerUnmodified = GetHeaderUnmodified(headers, key);
                if (headerUnmodified == null)
                {
                    SetHeaderUnmodified(headers, key, values);
                }
                else
                {
                    SetHeaderUnmodified(headers, key, headerUnmodified.Concat(values));
                }
            }
        }

        private static string DeQuote(string value)
        {
            if ((!string.IsNullOrWhiteSpace(value) && (value.Length > 1)) &&
                ((value[0] == '"') && (value[value.Length - 1] == '"')))
            {
                value = value.Substring(1, value.Length - 2);
            }
            return value;
        }

        internal static IDictionary<string, string> GetCookies(IOwinRequest request)
        {
            var dictionary = request.Get<IDictionary<string, string>>("Microsoft.Owin.Cookies#dictionary");
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
                request.Set("Microsoft.Owin.Cookies#dictionary", dictionary);
            }
            var header = GetHeader(request.Headers, "Cookie");
            if (request.Get<string>("Microsoft.Owin.Cookies#text") == header) return dictionary;
            dictionary.Clear();
            ParseDelimited(header, SemicolonAndComma, AddCookieCallback, dictionary);
            request.Set("Microsoft.Owin.Cookies#text", header);
            return dictionary;
        }

        internal static IFormCollection GetForm(string text)
        {
            IDictionary<string, string[]> store = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            var state = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            ParseDelimited(text, new[] {'&'}, AppendItemCallback, state);
            foreach (var pair in state)
            {
                store.Add(pair.Key, pair.Value.ToArray());
            }
            return new FormCollection(store);
        }

        public static string GetHeader(IDictionary<string, string[]> headers, string key)
        {
            var headerUnmodified = GetHeaderUnmodified(headers, key);
            if (headerUnmodified != null)
            {
                return string.Join(",", headerUnmodified);
            }
            return null;
        }

        public static IEnumerable<string> GetHeaderSplit(IDictionary<string, string[]> headers, string key)
        {
            var headerUnmodified = GetHeaderUnmodified(headers, key);
            if (headerUnmodified != null)
            {
                return GetHeaderSplitImplementation(headerUnmodified);
            }
            return null;
        }

        private static IEnumerable<string> GetHeaderSplitImplementation(string[] values)
        {
            return from iteratorVariable0 in new HeaderSegmentCollection(values)
                where iteratorVariable0.Data.HasValue
                select DeQuote(iteratorVariable0.Data.Value);
        }

        public static string[] GetHeaderUnmodified(IDictionary<string, string[]> headers, string key)
        {
            string[] strArray;
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            return !headers.TryGetValue(key, out strArray) ? null : strArray;
        }

        internal static string GetHost(IOwinRequest request)
        {
            var header = GetHeader(request.Headers, Constants.Headers.Host);
            if (!string.IsNullOrWhiteSpace(header))
            {
                return header;
            }
            var str2 = request.LocalIpAddress ?? "localhost";
            var str3 = request.Get<string>(OwinConstants.CommonKeys.LocalPort);
            if (!string.IsNullOrWhiteSpace(str3))
            {
                return (str2 + ":" + str3);
            }
            return str2;
        }

        internal static string GetJoinedValue(IDictionary<string, string[]> store, string key)
        {
            var unmodifiedValues = GetUnmodifiedValues(store, key);
            return unmodifiedValues != null ? string.Join(",", unmodifiedValues) : null;
        }

        internal static IDictionary<string, string[]> GetQuery(IOwinRequest request)
        {
            var dictionary = request.Get<IDictionary<string, string[]>>("Microsoft.Owin.Query#dictionary");
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
                request.Set("Microsoft.Owin.Query#dictionary", dictionary);
            }
            var text = request.QueryString.Value;
            if (request.Get<string>("Microsoft.Owin.Query#text") == text) return dictionary;
            dictionary.Clear();
            var state = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            ParseDelimited(text, AmpersandAndSemicolon, AppendItemCallback, state);
            foreach (var pair in state)
            {
                dictionary.Add(pair.Key, pair.Value.ToArray());
            }
            request.Set("Microsoft.Owin.Query#text", text);
            return dictionary;
        }

        internal static string[] GetUnmodifiedValues(IDictionary<string, string[]> store, string key)
        {
            string[] strArray;
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            return !store.TryGetValue(key, out strArray) ? null : strArray;
        }

        internal static void ParseDelimited(string text, char[] delimiters, Action<string, string, object> callback,
            object state)
        {
            int num4;
            var length = text.Length;
            var index = text.IndexOf('=');
            if (index == -1)
            {
                index = length;
            }
            for (var i = 0; i < length; i = num4 + 1)
            {
                num4 = text.IndexOfAny(delimiters, i);
                if (num4 == -1)
                {
                    num4 = length;
                }
                if (index >= num4) continue;
                while ((i != index) && char.IsWhiteSpace(text[i]))
                {
                    i++;
                }
                var str = text.Substring(i, index - i);
                var str2 = text.Substring(index + 1, (num4 - index) - 1);
                callback(Uri.UnescapeDataString(str.Replace('+', ' ')), Uri.UnescapeDataString(str2.Replace('+', ' ')),
                    state);
                index = text.IndexOf('=', num4);
                if (index == -1)
                {
                    index = length;
                }
            }
        }

        private static string QuoteIfNeeded(string value)
        {
            if ((!string.IsNullOrWhiteSpace(value) && value.Contains(',')) &&
                ((value[0] != '"') || (value[value.Length - 1] != '"')))
            {
                value = '"' + value + '"';
            }
            return value;
        }

        public static void SetHeader(IDictionary<string, string[]> headers, string key, string value)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                headers.Remove(key);
            }
            else
            {
                headers[key] = new[] {value};
            }
        }

        public static void SetHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if ((values == null) || (values.Length == 0))
            {
                headers.Remove(key);
            }
            else
            {
                headers[key] = new[] {string.Join(",", from value in values select QuoteIfNeeded(value))};
            }
        }

        public static void SetHeaderUnmodified(IDictionary<string, string[]> headers, string key,
            IEnumerable<string> values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            headers[key] = values.ToArray();
        }

        public static void SetHeaderUnmodified(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if ((values == null) || (values.Length == 0))
            {
                headers.Remove(key);
            }
            else
            {
                headers[key] = values;
            }
        }

    }
}

