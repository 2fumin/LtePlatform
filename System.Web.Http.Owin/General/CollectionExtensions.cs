using System.Collections.ObjectModel;
using System.Linq;

namespace System.Collections.Generic
{
    internal static class CollectionExtensions
    {
        public static T[] AppendAndReallocate<T>(this T[] array, T value)
        {
            var length = array.Length;
            var localArray = new T[length + 1];
            array.CopyTo(localArray, 0);
            localArray[length] = value;
            return localArray;
        }

        public static T[] AsArray<T>(this IEnumerable<T> values)
        {
            var localArray = values as T[];
            if (localArray == null)
            {
                localArray = values.ToArray();
            }
            return localArray;
        }

        public static Collection<T> AsCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = enumerable as Collection<T>;
            if (collection != null)
            {
                return collection;
            }
            var list = enumerable as IList<T>;
            if (list == null)
            {
                list = new List<T>(enumerable);
            }
            return new Collection<T>(list);
        }

        public static IList<T> AsIList<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable as IList<T>;
            if (list != null)
            {
                return list;
            }
            return new List<T>(enumerable);
        }

        public static List<T> AsList<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable as List<T>;
            if (list != null)
            {
                return list;
            }
            var wrappers = enumerable as ListWrapperCollection<T>;
            return wrappers != null ? wrappers.ItemsList : new List<T>(enumerable);
        }

        public static void RemoveFrom<T>(this List<T> list, int start)
        {
            list.RemoveRange(start, list.Count - start);
        }

        public static T SingleDefaultOrError<T, TArg1>(this IList<T> list, Action<TArg1> errorAction, TArg1 errorArg1)
        {
            switch (list.Count)
            {
                case 0:
                    return default(T);

                case 1:
                    return list[0];
            }
            errorAction(errorArg1);
            return default(T);
        }

        public static TMatch SingleOfTypeDefaultOrError<TInput, TMatch, TArg1>(this IList<TInput> list, Action<TArg1> errorAction, TArg1 errorArg1) 
            where TMatch : class
        {
            var local = default(TMatch);
            foreach (var local2 in list.OfType<TMatch>())
            {
                if (local != null)
                {
                    errorAction(errorArg1);
                    return default(TMatch);
                }
                local = local2;
            }
            return local;
        }

        public static T[] ToArrayWithoutNulls<T>(this ICollection<T> collection) where T : class
        {
            var sourceArray = new T[collection.Count];
            var index = 0;
            foreach (var local in collection.Where(local => local != null))
            {
                sourceArray[index] = local;
                index++;
            }
            if (index == collection.Count)
            {
                return sourceArray;
            }
            var destinationArray = new T[index];
            Array.Copy(sourceArray, destinationArray, index);
            return destinationArray;
        }

        public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IEnumerable<TValue> enumerable, 
            Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var array = enumerable as TValue[];
            if (array != null)
            {
                return array.ToDictionaryFast(keySelector, comparer);
            }
            var list = enumerable as IList<TValue>;
            return list != null ? ToDictionaryFastNoCheck(list, keySelector, comparer) : enumerable.ToDictionary(keySelector, comparer);
        }

        public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this IList<TValue> list, Func<TValue, TKey> keySelector, 
            IEqualityComparer<TKey> comparer)
        {
            var array = list as TValue[];
            return array != null ? array.ToDictionaryFast(keySelector, comparer) : ToDictionaryFastNoCheck(list, keySelector, comparer);
        }

        public static Dictionary<TKey, TValue> ToDictionaryFast<TKey, TValue>(this TValue[] array, Func<TValue, TKey> keySelector, 
            IEqualityComparer<TKey> comparer)
        {
            var dictionary = new Dictionary<TKey, TValue>(array.Length, comparer);
            foreach (var local in array)
            {
                dictionary.Add(keySelector(local), local);
            }
            return dictionary;
        }

        private static Dictionary<TKey, TValue> ToDictionaryFastNoCheck<TKey, TValue>(IList<TValue> list, Func<TValue, TKey> keySelector, 
            IEqualityComparer<TKey> comparer)
        {
            var count = list.Count;
            var dictionary = new Dictionary<TKey, TValue>(count, comparer);
            for (var i = 0; i < count; i++)
            {
                var local = list[i];
                dictionary.Add(keySelector(local), local);
            }
            return dictionary;
        }
    }
}
