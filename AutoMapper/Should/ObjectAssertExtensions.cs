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

        /// <summary>
        /// Verifies that two objects are equal, using a default comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="expected">The expected value</param>
        /// <exception cref="EqualException">Thrown when the objects are not equal</exception>
        public static void ShouldEqual<T>(this T actual,
                                          T expected)
        {
            CustomizeAssert.Equal(expected, actual);
        }

        /// <summary>
        /// Verifies that two objects are not equal, using a default comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="actual">The actual object</param>
        /// <param name="expected">The expected object</param>
        /// <exception cref="NotEqualException">Thrown when the objects are equal</exception>
        public static void ShouldNotEqual<T>(this T actual,
                                             T expected)
        {
            CustomizeAssert.NotEqual(expected, actual);
        }
    }
}