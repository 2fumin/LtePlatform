using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin
{
    public interface IReadableStringCollection : IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
    {
        string Get(string key);

        IList<string> GetValues(string key);

        string this[string key] { get; }
    }
}

