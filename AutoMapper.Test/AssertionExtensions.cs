using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Should;
using AutoMapper.Should.Core.Exceptions;
using Shouldly;

namespace AutoMapper.Test
{
    public delegate void ThrowingAction();

	public static class AssertionExtensions
	{
		public static void ShouldNotBeThrownBy(this Type exception, Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				if (exception.IsInstanceOfType(ex))
				{
					throw new AssertException($"Expected no exception of type {exception} to be thrown.", ex);
				}
			}
		}

        public static void ShouldContain<T>(this IEnumerable<T> items, T item)
        {
            item.ShouldBeOneOf(items.ToArray());
        }

        public static void ShouldBeThrownBy(this Type exceptionType, ThrowingAction action)
        {
            Exception e = null;

            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                e = ex;
            }

            e.ShouldNotBeNull();
            e.ShouldBeOfType(exceptionType);
        }

        public static void ShouldNotBeInstanceOf<TExpectedType>(this object actual)
        {
            actual.ShouldNotBeOfType<TExpectedType>();
        }
	}
}