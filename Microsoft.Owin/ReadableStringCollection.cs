using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    using Microsoft.Owin.Infrastructure;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ReadableStringCollection : IReadableStringCollection, IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
    {
        public ReadableStringCollection(IDictionary<string, string[]> store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            this.Store = store;
        }

        public string Get(string key)
        {
            return OwinHelpers.GetJoinedValue(this.Store, key);
        }

        public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
        {
            return this.Store.GetEnumerator();
        }

        public IList<string> GetValues(string key)
        {
            string[] strArray;
            this.Store.TryGetValue(key, out strArray);
            return strArray;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string this[string key]
        {
            get
            {
                return this.Get(key);
            }
        }

        private IDictionary<string, string[]> Store { get; set; }
    }
}
