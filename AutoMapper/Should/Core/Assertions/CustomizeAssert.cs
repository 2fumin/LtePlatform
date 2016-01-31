using System;
using System.Collections.Generic;
using AutoMapper.Should.Core.Exceptions;

namespace AutoMapper.Should.Core.Assertions
{
    /// <summary>
    /// Contains various static methods that are used to verify that conditions are met during the
    /// process of running tests.
    /// </summary>
    public class CustomizeAssert
    {
        /// <summary>
        /// Used by the Throws and DoesNotThrow methods.
        /// </summary>
        public delegate void ThrowsDelegate();

        /// <summary>
        /// Used by the Throws and DoesNotThrow methods.
        /// </summary>
        public delegate object ThrowsDelegateWithReturn();
        
        /// <summary>
        /// Verifies that two objects are equal, using a custom comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="comparer">The comparer used to compare the two objects</param>
        /// <exception cref="EqualException">Thrown when the objects are not equal</exception>
        public static void Equal<T>(T expected,
                                    T actual,
                                    IEqualityComparer<T> comparer)
        {
            if (!comparer.Equals(expected, actual))
                throw new EqualException(expected, actual);
        }

        /// <summary>
        /// Verifies that two doubles are equal within a tolerance range.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to compare against</param>
        /// <param name="tolerance">The +/- value for where the expected and actual are considered to be equal</param>
        public static void Equal(double expected, double actual, double tolerance)
        {
            var difference = Math.Abs(actual - expected);
            if (difference > tolerance)
                throw new EqualException(expected + " +/- " + tolerance, actual);
        }

        /// <summary>
        /// Verifies that two doubles are equal within a tolerance range.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to compare against</param>
        /// <param name="tolerance">The +/- value for where the expected and actual are considered to be equal</param>
        /// <param name="userMessage">The user message to be shown on failure</param>
        public static void Equal(double expected, double actual, double tolerance, string userMessage)
        {
            var difference = Math.Abs(actual - expected);
            if (difference > tolerance)
                throw new EqualException(expected + " +/- " + tolerance, actual, userMessage);
        }

        /// <summary>
        /// Verifies that two values are not equal, using a default comparer.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="tolerance">The +/- value for where the expected and actual are considered to be equal</param>
        /// <exception cref="NotEqualException">Thrown when the objects are equal</exception>
        public static void NotEqual(double expected, double actual, double tolerance)
        {
            var difference = Math.Abs(actual - expected);
            if (difference <= tolerance)
                throw new NotEqualException(expected + " +/- " + tolerance, actual);
        }

        /// <summary>
        /// Verifies that two values are not equal, using a default comparer.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="tolerance">The +/- value for where the expected and actual are considered to be equal</param>
        /// <param name="userMessage">The user message to be shown on failure</param>
        /// <exception cref="NotEqualException">Thrown when the objects are equal</exception>
        public static void NotEqual(double expected, double actual, double tolerance, string userMessage)
        {
            var difference = Math.Abs(actual - expected);
            if (difference <= tolerance)
                throw new NotEqualException(expected + " +/- " + tolerance, actual, userMessage);
        }

        /// <summary>
        /// Verifies that two dates are equal within a tolerance range.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to compare against</param>
        /// <param name="tolerance">The +/- value for where the expected and actual are considered to be equal</param>
        public static void Equal(DateTime expected, DateTime actual, TimeSpan tolerance)
        {
            var difference = Math.Abs((actual - expected).Ticks);
            if (difference > tolerance.Ticks)
                throw new EqualException(expected + " +/- " + tolerance, actual);
        }

        /// <summary>
        /// Verifies that two dates are not equal within a tolerance range.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to compare against</param>
        /// <param name="tolerance">The +/- value for where the expected and actual are considered to be equal</param>
        public static void NotEqual(DateTime expected, DateTime actual, TimeSpan tolerance)
        {
            var difference = Math.Abs((actual - expected).Ticks);
            if (difference <= tolerance.Ticks)
                throw new NotEqualException(expected + " +/- " + tolerance, actual);
        }

        /// <summary>
        /// Verifies that two dates are equal within a tolerance range.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to compare against</param>
        /// <param name="precision">The level of precision to use when making the comparison</param>
        public static void Equal(DateTime expected, DateTime actual, DatePrecision precision)
        {
            if (precision.Truncate(expected) != precision.Truncate(actual))
                throw new EqualException(precision.Truncate(actual), precision.Truncate(actual));
        }

        /// <summary>
        /// Verifies that two doubles are not equal within a tolerance range.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to compare against</param>
        /// <param name="precision">The level of precision to use when making the comparison</param>
        public static void NotEqual(DateTime expected, DateTime actual, DatePrecision precision)
        {
            if (precision.Truncate(expected) == precision.Truncate(actual))
                throw new NotEqualException(precision.Truncate(actual), precision.Truncate(actual));
        }

        /// <summary>
        /// Verifies that the exact exception is thrown (and not a derived exception type).
        /// </summary>
        /// <typeparam name="T">The type of the exception expected to be thrown</typeparam>
        /// <param name="testCode">A delegate to the code to be tested</param>
        /// <returns>The exception that was thrown, when successful</returns>
        /// <exception cref="ThrowsException">Thrown when an exception was not thrown, or when an exception of the incorrect type is thrown</exception>
        public static T Throws<T>(ThrowsDelegate testCode)
            where T : Exception
        {
            return (T)Throws(typeof(T), testCode);
        }

        /// <summary>
        /// Verifies that the exact exception is thrown (and not a derived exception type).
        /// </summary>
        /// <param name="exceptionType">The type of the exception expected to be thrown</param>
        /// <param name="testCode">A delegate to the code to be tested</param>
        /// <returns>The exception that was thrown, when successful</returns>
        /// <exception cref="ThrowsException">Thrown when an exception was not thrown, or when an exception of the incorrect type is thrown</exception>
        public static Exception Throws(Type exceptionType,
                                       ThrowsDelegate testCode)
        {
            var exception = Record.Exception(testCode);

            if (exception == null)
                throw new ThrowsException(exceptionType);

            if (!(exceptionType == exception.GetType()))
                throw new ThrowsException(exceptionType, exception);

            return exception;
        }
    }
}