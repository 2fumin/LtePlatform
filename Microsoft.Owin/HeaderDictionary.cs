using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin
{
    public class HeaderDictionary : IHeaderDictionary
    {
        public HeaderDictionary(IDictionary<string, string[]> store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            Store = store;
        }

        public void Add(KeyValuePair<string, string[]> item)
        {
            Store.Add(item);
        }

        public void Add(string key, string[] value)
        {
            Store.Add(key, value);
        }

        public void Append(string key, string value)
        {
            OwinHelpers.AppendHeader(Store, key, value);
        }

        public void AppendCommaSeparatedValues(string key, params string[] values)
        {
            OwinHelpers.AppendHeaderJoined(Store, key, values);
        }

        public void AppendValues(string key, params string[] values)
        {
            OwinHelpers.AppendHeaderUnmodified(Store, key, values);
        }

        public void Clear()
        {
            Store.Clear();
        }

        public bool Contains(KeyValuePair<string, string[]> item)
        {
            return Store.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Store.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string[]>[] array, int arrayIndex)
        {
            Store.CopyTo(array, arrayIndex);
        }

        public string Get(string key)
        {
            return OwinHelpers.GetHeader(Store, key);
        }

        public IList<string> GetCommaSeparatedValues(string key)
        {
            IEnumerable<string> headerSplit = OwinHelpers.GetHeaderSplit(Store, key);
            return headerSplit?.ToList();
        }

        public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
        {
            return Store.GetEnumerator();
        }

        public IList<string> GetValues(string key)
        {
            return OwinHelpers.GetHeaderUnmodified(Store, key);
        }

        public bool Remove(KeyValuePair<string, string[]> item)
        {
            return Store.Remove(item);
        }

        public bool Remove(string key)
        {
            return Store.Remove(key);
        }

        public void Set(string key, string value)
        {
            OwinHelpers.SetHeader(Store, key, value);
        }

        public void SetCommaSeparatedValues(string key, params string[] values)
        {
            OwinHelpers.SetHeaderJoined(Store, key, values);
        }

        public void SetValues(string key, params string[] values)
        {
            OwinHelpers.SetHeaderUnmodified(Store, key, values);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(string key, out string[] value)
        {
            return Store.TryGetValue(key, out value);
        }

        public int Count => Store.Count;

        public bool IsReadOnly => Store.IsReadOnly;

        public string this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Set(key, value);
            }
        }

        public ICollection<string> Keys => Store.Keys;

        private IDictionary<string, string[]> Store { get; }

        string[] IDictionary<string, string[]>.this[string key]
        {
            get
            {
                return Store[key];
            }
            set
            {
                Store[key] = value;
            }
        }

        public ICollection<string[]> Values => Store.Values;
    }
}
