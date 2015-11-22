using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Regular
{
    public static class SecureValue
    {
        public static string GetField(this IDataReader dataReader, string fieldName)
        {
            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                if (dataReader.GetName(i).Trim() == fieldName)
                {
                    return dataReader.GetValue(i).ToString().Trim();
                }
            }
            return "";
        }

        public static void ResetProperties<T>(this T source)
        {
            var properties = (typeof(T)).GetProperties();
            foreach (var property in properties.Where(property => property.CanWrite))
            {
                const int zero32 = 0;
                const short zero16 = 0;
                switch (property.PropertyType.Name)
                {
                    case "DateTime":
                        property.SetValue(source, DateTime.Now, null);
                        break;
                    case "Int16":
                        property.SetValue(source, zero16, null);
                        break;
                    default:
                        property.SetValue(source, zero32, null);
                        break;
                }
            }
        }

        public static void Increase<T>(this T source, T increment)
        {
            var properties = (typeof(T)).GetProperties();
            foreach (var property in properties.Where(property => property.CanWrite))
            {
                switch (property.PropertyType.Name)
                {
                    case "Int16":
                        var value16
                            = (short)((short)property.GetValue(increment, null) + (short)property.GetValue(source, null));
                        property.SetValue(source, value16, null);
                        break;
                    case "Int32":
                        property.SetValue(source,
                            (int)property.GetValue(increment, null) + (int)property.GetValue(source, null),
                            null);
                        break;
                    case "Int64":
                        property.SetValue(source,
                            (long)property.GetValue(increment, null) + (long)property.GetValue(source, null),
                            null);
                        break;
                    case "Double":
                        property.SetValue(source,
                            (double)property.GetValue(increment, null) + (double)property.GetValue(source, null),
                            null);
                        break;
                }
            }
        }

        public static void DividedBy<T>(this T source, int scalor)
        {
            var properties = (typeof(T)).GetProperties();

            foreach (var property in properties.Where(property => property.CanWrite))
            {
                switch (property.PropertyType.Name)
                {
                    case "Int16":
                        var value16 = (short)((short)property.GetValue(source, null) / scalor);
                        property.SetValue(source, value16, null);
                        break;
                    case "Int32":
                        property.SetValue(source, (int)property.GetValue(source, null) / scalor, null);
                        break;
                    case "Int64":
                        property.SetValue(source, (long)property.GetValue(source, null) / scalor, null);
                        break;
                    case "Double":
                        property.SetValue(source, (double)property.GetValue(source, null) / scalor, null);
                        break;
                }
            }
        }

        public static T Average<T>(this IEnumerable<T> sourceList)
            where T : class, new()
        {
            IEnumerable<T> enumerable = sourceList as T[] ?? sourceList.ToArray();
            var result = enumerable.ArraySum();
            result.DividedBy(enumerable.Count());
            return result;
        }

        public static T ArraySum<T>(this IEnumerable<T> sourceList) where T : class, new()
        {
            var result = new T();
            IEnumerable<T> enumerable = sourceList as T[] ?? sourceList.ToArray();
            enumerable.ElementAt(0).CloneDateTimeValue(result);
            foreach (var item in enumerable)
            {
                result.Increase(item);
            }
            return result;
        }

    }
}
