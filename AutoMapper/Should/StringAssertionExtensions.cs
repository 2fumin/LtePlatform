using System;
using AutoMapper.Should.Core.Assertions;
using AutoMapper.Should.Core.Exceptions;

namespace AutoMapper.Should
{
    /// <summary>
    /// Extensions which provide assertions to classes derived from <see cref="String"/>.
    /// </summary>
    public static class StringAssertionExtensions
    {
        /// <summary>
        /// Verifies that a string contains a given sub-string, using the current culture.
        /// </summary>
        /// <param name="actualString">The string to be inspected</param>
        /// <param name="expectedSubString">The sub-string expected to be in the string</param>
        /// <exception cref="ContainsException">Thrown when the sub-string is not present inside the string</exception>
        public static int ShouldContain(this string actualString,
                                         string expectedSubString)
        {
            return CustomizeAssert.Contains(expectedSubString, actualString);
        }

        /// <summary>
        /// Verifies that a string contains a given sub-string, using the given comparison type.
        /// </summary>
        /// <param name="actualString">The string to be inspected</param>
        /// <param name="expectedSubString">The sub-string expected to be in the string</param>
        /// <param name="comparisonType">The type of string comparison to perform</param>
        /// <exception cref="ContainsException">Thrown when the sub-string is not present inside the string</exception>
        public static void ShouldContain(this string actualString,
                                         string expectedSubString,
                                         StringComparison comparisonType)
        {
            CustomizeAssert.Contains(expectedSubString, actualString, comparisonType);
        }

        /// <summary>
        /// Verifies that a string does not contain a given sub-string, using the current culture.
        /// </summary>
        /// <param name="actualString">The string to be inspected</param>
        /// <param name="expectedSubString">The sub-string which is expected not to be in the string</param>
        /// <exception cref="DoesNotContainException">Thrown when the sub-string is present inside the string</exception>
        public static void ShouldNotContain(this string actualString,
                                            string expectedSubString)
        {
            CustomizeAssert.DoesNotContain(expectedSubString, actualString);
        }

        /// <summary>
        /// Verifies that a string does not contain a given sub-string, using the current culture.
        /// </summary>
        /// <param name="actualString">The string to be inspected</param>
        /// <param name="expectedSubString">The sub-string which is expected not to be in the string</param>
        /// <param name="comparisonType">The type of string comparison to perform</param>
        /// <exception cref="DoesNotContainException">Thrown when the sub-string is present inside the given string</exception>
        public static void ShouldNotContain(this string actualString,
                                            string expectedSubString,
                                            StringComparison comparisonType)
        {
            CustomizeAssert.DoesNotContain(expectedSubString, actualString, comparisonType);
        }

        public static void ShouldStartWith(this string actualString,
                                           string expectedStartString)
        {
            CustomizeAssert.StartsWith(expectedStartString, actualString);
        }
    }
}