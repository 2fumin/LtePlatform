using System.ComponentModel;
using System.Linq;

namespace System.Collections.Generic
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class DictionaryExtensions
    {
        internal static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(
            this IDictionary<string, TValue> dictionary, string prefix)
        {
            TValue iteratorVariable0;
            if (dictionary.TryGetValue(prefix, out iteratorVariable0))
            {
                yield return new KeyValuePair<string, TValue>(prefix, iteratorVariable0);
            }
            var enumerator = dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                var key = current.Key;
                if ((key.Length > prefix.Length) && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    if (prefix.Length == 0)
                    {
                        yield return current;
                    }
                    else
                    {
                        switch (key[prefix.Length])
                        {
                            case '.':
                            case '[':
                            {
                                yield return current;
                                continue;
                            }
                        }
                    }
                }
            }
        }

        public static void RemoveFromDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, bool> removeCondition)
        {
            dictionary.RemoveFromDictionary(
                (entry, innerCondition) => innerCondition(entry), removeCondition);
        }

        public static void RemoveFromDictionary<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, TState, bool> removeCondition, TState state)
        {
            var index = 0;
            var localArray = new TKey[dictionary.Count];
            foreach (var pair in dictionary.Where(pair => removeCondition(pair, state)))
            {
                localArray[index] = pair.Key;
                index++;
            }
            for (var i = 0; i < index; i++)
            {
                dictionary.Remove(localArray[i]);
            }
        }

        public static bool TryGetValue<T>(this IDictionary<string, object> collection, string key, out T value)
        {
            object obj2;
            if (collection.TryGetValue(key, out obj2) && (obj2 is T))
            {
                value = (T) obj2;
                return true;
            }
            value = default(T);
            return false;
        }
    }
}
