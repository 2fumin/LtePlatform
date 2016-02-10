using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin
{
    public class RequestCookieCollection : IEnumerable<KeyValuePair<string, string>>, IEnumerable
    {
        public RequestCookieCollection(IDictionary<string, string> store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            Store = store;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return Store.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string this[string key]
        {
            get
            {
                string str;
                Store.TryGetValue(key, out str);
                return str;
            }
        }

        private IDictionary<string, string> Store { get; }
    }
}
