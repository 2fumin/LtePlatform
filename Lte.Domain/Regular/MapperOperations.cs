using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Regular
{
    public static class MapperOperations
    {
        public static void CloneDateTimeValue<T>(this T source, T destination)
            where T : class, new()
        {
            var properties = (typeof(T)).GetProperties();
            foreach (var property in properties
                .Where(property => property.CanRead && property.CanWrite)
                .Where(property => property.PropertyType.Name == "DateTime"))
            {
                property.SetValue(destination, property.GetValue(source, null), null);
            }
        }

        public static void CloneProperties<T>(this T source, T destination)
            where T : class
        {
            var properties = (typeof(T)).GetProperties();
            foreach (var property in properties
                .Where(property => property.CanRead && property.CanWrite))
            {
                property.SetValue(destination, property.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// 安全的对象属性克隆方法（源对象和目标对象具有不同类型）
        /// </summary>
        /// <typeparam name="TSource">源对象类型</typeparam>
        /// <typeparam name="TDest">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="dest">目标对象</param>
        /// <param name="protectionConsidered"></param>
        public static void CloneProperties<TSource, TDest>(this TSource source, TDest dest, bool protectionConsidered = false)
            where TSource : class, new()
            where TDest : class, new()
        {
            var srcProperties = (typeof(TSource)).GetProperties();
            var destProperties = (typeof(TDest)).GetProperties();

            foreach (var srcProperty in srcProperties.Where(srcProperty => srcProperty.CanRead))
            {
                if (protectionConsidered)
                {
                    var cfa = (CloneProtectionAttribute)Attribute.GetCustomAttribute(
                        srcProperty, typeof(CloneProtectionAttribute));
                    if (cfa != null) { continue; }
                }
                foreach (var destProperty in destProperties
                    .Where(destProperty => destProperty.Name == srcProperty.Name
                                           && destProperty.CanWrite
                                           && (destProperty.PropertyType == srcProperty.PropertyType
                                               || (destProperty.PropertyType.Name == "Int64"
                                                   && (srcProperty.PropertyType.Name == "Int32"
                                                       || srcProperty.PropertyType.Name == "Int16")))))
                {
                    destProperty.SetValue(dest, srcProperty.GetValue(source, null), null);
                }
            }
        }
    }
}
