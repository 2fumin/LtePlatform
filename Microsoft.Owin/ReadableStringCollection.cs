using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin
{
    public class ReadableStringCollection : IReadableStringCollection, IEnumerable
    {
        public ReadableStringCollection(IDictionary<string, string[]> store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            Store = store;
        }

        public string Get(string key)
        {
            return OwinHelpers.GetJoinedValue(Store, key);
        }

        public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
        {
            return Store.GetEnumerator();
        }

        public IList<string> GetValues(string key)
        {
            string[] strArray;
            Store.TryGetValue(key, out strArray);
            return strArray;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string this[string key] => Get(key);

        private IDictionary<string, string[]> Store { get; }
    }
}
