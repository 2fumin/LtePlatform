using System.Collections.Generic;

namespace Microsoft.Owin
{
    public class FormCollection : ReadableStringCollection, IFormCollection
    {
        public FormCollection(IDictionary<string, string[]> store) : base(store)
        {
        }
    }
}
