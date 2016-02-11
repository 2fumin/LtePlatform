namespace System.Collections.Generic
{
    internal static class DictionaryExtensions
    {
        internal static T Get<T>(this IDictionary<string, object> dictionary, string key, T fallback = default(T))
        {
            object obj2;
            if (!dictionary.TryGetValue(key, out obj2))
            {
                return fallback;
            }
            return (T)obj2;
        }
    }
}
