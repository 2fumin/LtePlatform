using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Owin.Host.SystemWeb.CallEnvironment
{
    internal class NilDictionary : IDictionary<string, object>
    {
        private static readonly string[] EmptyKeys = new string[0];
        private static readonly IEnumerable<KeyValuePair<string, object>> EmptyKeyValuePairs = Enumerable.Empty<KeyValuePair<string, object>>();
        private static readonly object[] EmptyValues = new object[0];

        public void Add(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return false;
        }

        public bool ContainsKey(string key)
        {
            return false;
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return EmptyKeyValuePairs.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return false;
        }

        public bool Remove(string key)
        {
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return EmptyKeyValuePairs.GetEnumerator();
        }

        public bool TryGetValue(string key, out object value)
        {
            value = null;
            return false;
        }

        public int Count => 0;

        public bool IsReadOnly => false;

        public object this[string key]
        {
            get
            {
                throw new KeyNotFoundException(key);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<string> Keys => EmptyKeys;

        public ICollection<object> Values => EmptyValues;
    }
}
