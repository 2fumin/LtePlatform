using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Lte.Domain.LinqToExcel.Service
{
    public static class ExcelCommonExtensions
    {
        /// <summary>
        /// Sets the value of a property
        /// </summary>
        /// <param name="object"></param>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="value">Value to set the property to</param>
        public static void SetProperty(this object @object, string propertyName, object value)
        {
            @object.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, @object, new[] { value });
        }

        /// <summary>
        /// Calls a method
        /// </summary>
        /// <param name="object"></param>
        /// <param name="methodName">Name of the method</param>
        /// <param name="args">Method arguments</param>
        /// <returns>Return value of the method</returns>
        public static void CallMethod(this object @object, string methodName, params object[] args)
        {
            @object.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, @object, args);
        }

        public static string[] ToArray(this ICollection<string> collection)
        {
            return Enumerable.ToArray(collection);
        }

        public static bool IsNumber(this string value)
        {
            return Regex.Match(value, @"^\d+$").Success;
        }

        public static bool IsNullValue(this Expression exp)
        {
            return ((exp is ConstantExpression) &&
                (exp.Cast<ConstantExpression>().Value == null));
        }

        public static string RegexReplace(this string source, string regex, string replacement)
        {
            return Regex.Replace(source, regex, replacement);
        }
    }
}
