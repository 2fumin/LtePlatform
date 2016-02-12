using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb.CallHeaders
{
    internal class AspNetRequestHeaders : IDictionary<string, string[]>
    {
        private readonly HttpRequestBase _httpRequest;

        internal AspNetRequestHeaders(HttpRequestBase httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public void Add(KeyValuePair<string, string[]> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(string key, string[] value)
        {
            if (ContainsKey(key))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_DuplicateKey, key));
            }
            Add(key, value, 0);
        }

        private void Add(string key, string[] value, int offset)
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
                Headers.Add(key, value[offset]);
                offset++;
            }
        }

        public void Clear()
        {
            Headers.Clear();
        }

        public bool Contains(KeyValuePair<string, string[]> item)
        {
            string[] strArray;
            return (TryGetValue(item.Key, out strArray) && ReferenceEquals(item.Value, strArray));
        }

        public bool ContainsKey(string key)
        {
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
            return Headers.GetValues(key);
        }

        public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
        {
            var index = 0;
            while (true)
            {
                if (index >= Headers.Count)
                {
                    yield break;
                }
                yield return new KeyValuePair<string, string[]>(Headers.Keys[index], Headers.GetValues(index));
                index++;
            }
        }

        public bool Remove(KeyValuePair<string, string[]> item)
        {
            return (Contains(item) && Remove(item.Key));
        }

        public bool Remove(string key)
        {
            if (!ContainsKey(key)) return false;
            Headers.Remove(key);
            return true;
        }

        private void Set(string key, string[] values)
        {
            if ((values == null) || (values.Length == 0))
            {
                Headers.Remove(key);
            }
            else
            {
                Headers.Set(key, values[0]);
                Add(key, values, 1);
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

        public int Count => Headers.Count;

        private NameValueCollection Headers => _httpRequest.Headers;

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

        public ICollection<string> Keys => Headers.AllKeys;

        public ICollection<string[]> Values
            => (from key in Headers.AllKeys select Headers.GetValues(key)).ToList<string[]>();
    }
}
