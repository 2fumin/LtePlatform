﻿using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper.Internal;

namespace AutoMapper.Should.Core.Assertions
{
    internal class AssertEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            var type = typeof(T);

            // Null?
            if (!type.IsValueType() || (type.IsGenericType() && type.GetGenericTypeDefinition().IsAssignableFrom(typeof(Nullable<>))))
            {
                if (object.Equals(x, default(T)))
                    return object.Equals(y, default(T));

                if (object.Equals(y, default(T)))
                    return false;
            }

            //x implements IEquitable<T> and is assignable from y?
            var xIsAssignableFromY = x.GetType().IsInstanceOfType(y);
            if (xIsAssignableFromY && x is IEquatable<T>)
                return ((IEquatable<T>)x).Equals(y);

            //y implements IEquitable<T> and is assignable from x?
            var yIsAssignableFromX = y.GetType().IsInstanceOfType(x);
            if (yIsAssignableFromX && y is IEquatable<T>)
                return ((IEquatable<T>)y).Equals(x);

            // Enumerable?
            var enumerableX = x as IEnumerable;
            var enumerableY = y as IEnumerable;

            if (enumerableX != null && enumerableY != null)
            {
                return new EnumerableEqualityComparer().Equals(enumerableX, enumerableY);
            }

            // Last case, rely on Object.Equals
            return object.Equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }
    }
}