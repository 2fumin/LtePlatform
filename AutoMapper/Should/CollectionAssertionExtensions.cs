using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Should.Core.Assertions;
using AutoMapper.Should.Core.Exceptions;

namespace AutoMapper.Should
{
    /// <summary>
    /// Extensions which provide assertions to classes derived from <see cref="IEnumerable"/> and <see cref="IEnumerable&lt;T&gt;"/>.
    /// </summary>
    public static class CollectionAssertExtensions
    {
        /// <summary>
        /// Verifies that a collection contains a given object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be verified</typeparam>
        /// <param name="collection">The collection to be inspected</param>
        /// <param name="expected">The object expected to be in the collection</param>
        /// <exception cref="ContainsException">Thrown when the object is not present in the collection</exception>
        public static void ShouldContain<T>(this IEnumerable<T> collection,
                                            T expected)
        {
            CustomizeAssert.Contains(expected, collection);
        }
    }
}