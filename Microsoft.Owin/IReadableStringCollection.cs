using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin
{
    /// <summary>
    /// Accessors for headers, query, forms, etc.
    /// </summary>
    public interface IReadableStringCollection : IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
    {
        /// <summary>
        /// Get the associated value from the collection.  Multiple values will be merged.
        /// Returns null if the key is not present.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// Get the associated values from the collection in their original format.
        /// Returns null if the key is not present.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IList<string> GetValues(string key);

        /// <summary>
        /// Get the associated value from the collection.  Multiple values will be merged.
        /// Returns null if the key is not present.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string this[string key] { get; }
    }
}

