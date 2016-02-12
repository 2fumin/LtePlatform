using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb.CallHeaders
{
    internal class AspNetResponseHeaders : IDictionary<string, string[]>
    {
        private readonly NameValueCollection _headers;
        private readonly HttpResponseBase _response;

        internal AspNetResponseHeaders(HttpResponseBase response)
        {
            _response = response;
            _headers = response.Headers;
        }

        public void Add(KeyValuePair<string, string[]> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(string key, string[] value)
        {
            if (ContainsKey(key))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_DuplicateKey,
                    key));
            }
            Append(key, value, 0);
        }

        private void Append(string key, string[] value, int offset)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            while (offset < value.Length)
            {
                _response.AppendHeader(key, value[offset]);
                offset++;
            }
        }

        public void Clear()
        {
            _response.ClearHeaders();
            _headers.Clear();
        }

        public bool Contains(KeyValuePair<string, string[]> item)
        {
            string[] strArray;
            return (TryGetValue(item.Key, out strArray) && ReferenceEquals(item.Value, strArray));
        }

        public bool ContainsKey(string key)
        {
            if (Constants.Headers.ContentType.Equals(key, StringComparison.OrdinalIgnoreCase))
            {
                return !string.IsNullOrEmpty(_response.ContentType);
            }
            return Keys.Contains(key, StringComparer.OrdinalIgnoreCase);
        }

        public void CopyTo(KeyValuePair<string, string[]>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (arrayIndex > (Count - array.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex, string.Empty);
            }
            foreach (var pair in this)
            {
                array[arrayIndex++] = pair;
            }
        }

        private string[] Get(string key)
        {
            if (!Constants.Headers.ContentType.Equals(key, StringComparison.OrdinalIgnoreCase))
            {
                return _headers.GetValues(key);
            }
            var contentType = _response.ContentType;
            return string.IsNullOrEmpty(contentType) ? null : new[] {contentType};
        }

        public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
        {
            if (!string.IsNullOrEmpty(_response.ContentType))
            {
                yield return
                    new KeyValuePair<string, string[]>(Constants.Headers.ContentType, new[] {_response.ContentType});
            }
            for (var i = 0; i < _headers.Count; i++)
            {
                var iteratorVariable1 = _headers.Keys[i];
                if (!Constants.Headers.ContentType.Equals(iteratorVariable1, StringComparison.OrdinalIgnoreCase))
                {
                    yield return new KeyValuePair<string, string[]>(iteratorVariable1, _headers.GetValues(i));
                }
            }
        }

        public bool Remove(KeyValuePair<string, string[]> item)
        {
            return (Contains(item) && Remove(item.Key));
        }

        public bool Remove(string key)
        {
            if (!ContainsKey(key)) return false;
            Set(key, null);
            return true;
        }

        private void Set(string key, string[] values)
        {
            if (Constants.Headers.ContentType.Equals(key, StringComparison.OrdinalIgnoreCase) ||
                Constants.Headers.CacheControl.Equals(key, StringComparison.OrdinalIgnoreCase))
            {
                _headers.Remove(key);
                if (values != null)
                {
                    Append(key, values, 0);
                }
            }
            else if ((values == null) || (values.Length == 0))
            {
                _headers.Remove(key);
            }
            else
            {
                _headers.Set(key, values[0]);
                Append(key, values, 1);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(string key, out string[] value)
        {
            value = Get(key);
            return (value != null);
        }

        public int Count => _headers.Count;

        public bool IsReadOnly => false;

        public string[] this[string key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                var strArray = Get(key);
                if (strArray == null)
                {
                    throw new KeyNotFoundException(key);
                }
                return strArray;
            }
            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                Set(key, value);
            }
        }

        public ICollection<string> Keys => _headers.AllKeys;

        public ICollection<string[]> Values => (from key in _headers.AllKeys select _headers.GetValues(key)).ToList<string[]>();
    }
}
