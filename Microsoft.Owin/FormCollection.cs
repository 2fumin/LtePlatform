using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FormCollection : ReadableStringCollection, IFormCollection, IReadableStringCollection, IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
    {
        public FormCollection(IDictionary<string, string[]> store) : base(store)
        {
        }
    }
}
