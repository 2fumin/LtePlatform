using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
            AddCookieCallback = delegate (string name, string value, object state) {
                IDictionary<string, string> dictionary = (IDictionary<string, string>)state;
                if (!dictionary.ContainsKey(name))
                {
                    dictionary.Add(name, value);
                }
            };
            SemicolonAndComma = new[] { ';', ',' };
            AppendItemCallback = delegate (string name, string value, object state) {
                List<string> list;
                IDictionary<string, List<string>> dictionary = (IDictionary<string, List<string>>)state;
                if (!dictionary.TryGetValue(name, out list))
                {
                    dictionary.Add(name, new List<string>(1) { value });
                }
                else
                {
                    list.Add(value);
                }
            };
            AmpersandAndSemicolon = new[] { '&', ';' };
        }

        public static void AppendHeader(IDictionary<string, string[]> headers, string key, string values)
        {
            if (!string.IsNullOrWhiteSpace(values))
            {
                string header = GetHeader(headers, key);
                if (header == null)
                {
                    SetHeader(headers, key, values);
                }
                else
                {
                    headers[key] = new[] { header + "," + values };
                }
            }
        }

        public static void AppendHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if ((values != null) && (values.Length != 0))
            {
                string header = GetHeader(headers, key);
                if (header == null)
                {
                    SetHeaderJoined(headers, key, values);
                }
                else
                {
                    headers[key] = new[] { header + "," + string.Join(",", (IEnumerable<string>)(from value in values select QuoteIfNeeded(value))) };
                }
            }
        }

        public static void AppendHeaderUnmodified(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if ((values != null) && (values.Length != 0))
            {
                string[] headerUnmodified = GetHeaderUnmodified(headers, key);
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
            if ((!string.IsNullOrWhiteSpace(value) && (value.Length > 1)) && ((value[0] == '"') && (value[value.Length - 1] == '"')))
            {
                value = value.Substring(1, value.Length - 2);
            }
            return value;
        }

        internal static IDictionary<string, string> GetCookies(IOwinRequest request)
        {
            IDictionary<string, string> dictionary = request.Get<IDictionary<string, string>>("Microsoft.Owin.Cookies#dictionary");
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
                request.Set("Microsoft.Owin.Cookies#dictionary", dictionary);
            }
            string header = GetHeader(request.Headers, "Cookie");
            if (request.Get<string>("Microsoft.Owin.Cookies#text") == header) return dictionary;
            dictionary.Clear();
            ParseDelimited(header, SemicolonAndComma, AddCookieCallback, dictionary);
            request.Set("Microsoft.Owin.Cookies#text", header);
            return dictionary;
        }

        internal static IFormCollection GetForm(string text)
        {
            IDictionary<string, string[]> store = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, List<string>> state = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            ParseDelimited(text, new[] { '&' }, AppendItemCallback, state);
            foreach (KeyValuePair<string, List<string>> pair in state)
            {
                store.Add(pair.Key, pair.Value.ToArray());
            }
            return new FormCollection(store);
        }

        public static string GetHeader(IDictionary<string, string[]> headers, string key)
        {
            string[] headerUnmodified = GetHeaderUnmodified(headers, key);
            if (headerUnmodified != null)
            {
                return string.Join(",", headerUnmodified);
            }
            return null;
        }

        public static IEnumerable<string> GetHeaderSplit(IDictionary<string, string[]> headers, string key)
        {
            string[] headerUnmodified = GetHeaderUnmodified(headers, key);
            if (headerUnmodified != null)
            {
                return GetHeaderSplitImplementation(headerUnmodified);
            }
            return null;
        }

        private static IEnumerable<string> GetHeaderSplitImplementation(string[] values)
        {
            foreach (HeaderSegment iteratorVariable0 in new HeaderSegmentCollection(values))
            {
                if (iteratorVariable0.Data.HasValue)
                {
                    yield return DeQuote(iteratorVariable0.Data.Value);
                }
            }
        }

        public static string[] GetHeaderUnmodified(IDictionary<string, string[]> headers, string key)
        {
            string[] strArray;
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            if (!headers.TryGetValue(key, out strArray))
            {
                return null;
            }
            return strArray;
        }

        internal static string GetHost(IOwinRequest request)
        {
            string header = GetHeader(request.Headers, "Host");
            if (!string.IsNullOrWhiteSpace(header))
            {
                return header;
            }
            string str2 = request.LocalIpAddress ?? "localhost";
            string str3 = request.Get<string>("server.LocalPort");
            if (!string.IsNullOrWhiteSpace(str3))
            {
                return (str2 + ":" + str3);
            }
            return str2;
        }

        internal static string GetJoinedValue(IDictionary<string, string[]> store, string key)
        {
            string[] unmodifiedValues = GetUnmodifiedValues(store, key);
            if (unmodifiedValues != null)
            {
                return string.Join(",", unmodifiedValues);
            }
            return null;
        }

        internal static IDictionary<string, string[]> GetQuery(IOwinRequest request)
        {
            IDictionary<string, string[]> dictionary = request.Get<IDictionary<string, string[]>>("Microsoft.Owin.Query#dictionary");
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
                request.Set("Microsoft.Owin.Query#dictionary", dictionary);
            }
            string text = request.QueryString.Value;
            if (request.Get<string>("Microsoft.Owin.Query#text") != text)
            {
                dictionary.Clear();
                Dictionary<string, List<string>> state = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
                ParseDelimited(text, AmpersandAndSemicolon, AppendItemCallback, state);
                foreach (KeyValuePair<string, List<string>> pair in state)
                {
                    dictionary.Add(pair.Key, pair.Value.ToArray());
                }
                request.Set("Microsoft.Owin.Query#text", text);
            }
            return dictionary;
        }

        internal static string[] GetUnmodifiedValues(IDictionary<string, string[]> store, string key)
        {
            string[] strArray;
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            if (!store.TryGetValue(key, out strArray))
            {
                return null;
            }
            return strArray;
        }

        internal static void ParseDelimited(string text, char[] delimiters, Action<string, string, object> callback, object state)
        {
            int num4;
            int length = text.Length;
            int index = text.IndexOf('=');
            if (index == -1)
            {
                index = length;
            }
            for (int i = 0; i < length; i = num4 + 1)
            {
                num4 = text.IndexOfAny(delimiters, i);
                if (num4 == -1)
                {
                    num4 = length;
                }
                if (index < num4)
                {
                    while ((i != index) && char.IsWhiteSpace(text[i]))
                    {
                        i++;
                    }
                    string str = text.Substring(i, index - i);
                    string str2 = text.Substring(index + 1, (num4 - index) - 1);
                    callback(Uri.UnescapeDataString(str.Replace('+', ' ')), Uri.UnescapeDataString(str2.Replace('+', ' ')), state);
                    index = text.IndexOf('=', num4);
                    if (index == -1)
                    {
                        index = length;
                    }
                }
            }
        }

        private static string QuoteIfNeeded(string value)
        {
            if ((!string.IsNullOrWhiteSpace(value) && value.Contains(',')) && ((value[0] != '"') || (value[value.Length - 1] != '"')))
            {
                value = '"' + value + '"';
            }
            return value;
        }

        public static void SetHeader(IDictionary<string, string[]> headers, string key, string value)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                headers.Remove(key);
            }
            else
            {
                headers[key] = new[] { value };
            }
        }

        public static void SetHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            if ((values == null) || (values.Length == 0))
            {
                headers.Remove(key);
            }
            else
            {
                headers[key] = new[] { string.Join(",", (IEnumerable<string>)(from value in values select QuoteIfNeeded(value))) };
            }
        }

        public static void SetHeaderUnmodified(IDictionary<string, string[]> headers, string key, IEnumerable<string> values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            headers[key] = values.ToArray();
        }

        public static void SetHeaderUnmodified(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
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

        [CompilerGenerated]
        private sealed class <GetHeaderSplitImplementation>d__0 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
        {
            private int <>1__state;
            private string <>2__current;
            public string[] <>3__values;
            public HeaderSegmentCollection.Enumerator<>7__wrap2;
            private int <>l__initialThreadId;
            public HeaderSegment<segment>5__1;
            public string[] values;

        [DebuggerHidden]
        public <GetHeaderSplitImplementation>d__0(int <>1__state)
        {
            this.<> 1__state = <> 1__state;
            this.<> l__initialThreadId = Environment.CurrentManagedThreadId;
        }

        private void <>m__Finally3()
        {
            this.<> 1__state = -1;
            this.<> 7__wrap2.Dispose();
        }

        private bool MoveNext()
        {
            bool flag;
            try
            {
                switch (this.<> 1__state)
                    {
                        case 0:
                            this.<> 1__state = -1;
                    this.<> 7__wrap2 = new HeaderSegmentCollection(this.values).GetEnumerator();
                    this.<> 1__state = 1;
                    goto Label_00A6;

                        case 2:
                            this.<> 1__state = 1;
                    goto Label_00A6;

                    default:
                            goto Label_00B9;
                }
                Label_0047:
                this.< segment > 5__1 = this.<> 7__wrap2.Current;
                if (this.< segment > 5__1.Data.HasValue)
                    {
                    this.<> 2__current = OwinHelpers.DeQuote(this.< segment > 5__1.Data.Value);
                    this.<> 1__state = 2;
                    return true;
                }
                Label_00A6:
                if (this.<> 7__wrap2.MoveNext())
                    {
                    goto Label_0047;
                }
                this.<> m__Finally3();
                Label_00B9:
                flag = false;
            }
                fault
                {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

        [DebuggerHidden]
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            OwinHelpers.< GetHeaderSplitImplementation > d__0 d__;
            if ((Environment.CurrentManagedThreadId == this.<> l__initialThreadId) && (this.<> 1__state == -2))
                {
                this.<> 1__state = 0;
                d__ = this;
            }
                else
                {
                d__ = new OwinHelpers.< GetHeaderSplitImplementation > d__0(0);
            }
            d__.values = this.<> 3__values;
            return d__;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<String>.GetEnumerator();
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
            switch (this.<> 1__state)
                {
                    case 1:
                    case 2:
                        try
                {
                }
                finally
                {
                    this.<> m__Finally3();
                }
                return;
            }
        }

        string IEnumerator<string>.Current
        {
            [DebuggerHidden]
            get
            {
                return this.<> 2__current;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return this.<> 2__current;
            }
        }
    }
}
}

