using System;
using AutoMapper.Should.Core.Assertions;
using AutoMapper.Should.Core.Exceptions;

namespace AutoMapper.Should
{
    /// <summary>
    /// Extensions which provide assertions to classes derived from <see cref="object"/>.
    /// </summary>
    public static class ObjectAssertExtensions
    {
        /// <summary>
        /// Verifies that an object is exactly the given type (and not a derived type).
        /// </summary>
        /// <typeparam name="T">The type the object should be</typeparam>
        /// <param name="object">The object to be evaluated</param>
        /// <returns>The object, casted to type T when successful</returns>
        /// <exception cref="IsTypeException">Thrown when the object is not the given type</exception>
        public static T ShouldBeType<T>(this object @object)
        {
            return CustomizeAssert.IsType<T>(@object);
        }

        /// <summary>
        /// Verifies that an object is exactly the given type (and not a derived type).
        /// </summary>
        /// <param name="object">The object to be evaluated</param>
        /// <param name="expectedType">The type the object should be</param>
        /// <exception cref="IsTypeException">Thrown when the object is not the given type</exception>
        public static void ShouldBeType(this object @object,
                                        Type expectedType)
        {
            CustomizeAssert.IsType(expectedType, @object);
        }
    }
}