using System;
using AutoMapper.Should.Core.Assertions;

namespace AutoMapper.Should
{
    public static class ActionAssertionExtensions
    {
        /// <summary>Verifies that the <paramref name="action"/> throws the specified exception type.</summary>
        /// <typeparam name="T">The type of exception expected to be thrown.</typeparam>
        /// <param name="action">The action which should throw the exception.</param>
        /// <param name="exceptionChecker">Additional checks on the exception object.</param>
        public static void ShouldThrow<T>(this Action action, Action<T> exceptionChecker = null) where T : Exception
        {
            ShouldThrow<T>(new CustomizeAssert.ThrowsDelegate(action), exceptionChecker);
        }

        /// <summary>Verifies that the <paramref name="@delegate"/> throws the specified exception type.</summary>
        /// <typeparam name="T">The type of exception expected to be thrown.</typeparam>
        /// <param name="delegate">A <see cref="CustomizeAssert.ThrowsDelegate"/> which represents the action which should throw the exception.</param>
        /// <param name="exceptionChecker">Additional checks on the exception object.</param>
        public static void ShouldThrow<T>(this CustomizeAssert.ThrowsDelegate @delegate, Action<T> exceptionChecker = null) where T : Exception
        {
            var exception = CustomizeAssert.Throws<T>(@delegate);
            exceptionChecker?.Invoke(exception);
        }
    }
}