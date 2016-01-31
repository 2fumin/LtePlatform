using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
	public class LazyCollectionMapping
	{
        public LazyCollectionMapping()
        {
            SetUp();
        }
		public void SetUp()
		{
			Mapper.Reset();
		}

		public class OneTimeEnumerator<T> : IEnumerable<T>
		{
			private readonly IEnumerable<T> inner;

			public OneTimeEnumerator(IEnumerable<T> inner)
			{
				this.inner = inner;
			}

			private bool isEnumerated;

			public IEnumerator<T> GetEnumerator()
			{
				if (isEnumerated)
					throw new NotSupportedException();
				isEnumerated = true;
				return inner.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		public class Source
		{
			public IEnumerable<string> Collection { get; set; }
		}

		public class Destination
		{
			public IEnumerable<string> Collection { get; set; }
		}

		[Test]
		public void OneTimeEnumerator_should_throw_exception_if_enumerating_twice()
		{
			var enumerable = Create(new[] {"one", "two", "three"});
			
			enumerable.Count().ShouldBe(3);

			typeof (NotSupportedException).ShouldBeThrownBy(() => enumerable.Count());
		}
		
		[Test]
		public void Should_not_enumerate_twice()
		{
			Mapper.CreateMap<Source, Destination>();

			var source = new Source {Collection = Create(new[] {"one", "two", "three"})};
			var enumerable = Mapper.Map(source, new Destination());

			enumerable.Collection.Count().ShouldBe(3);
		}

		public static IEnumerable<T> Create<T>(IEnumerable<T> inner)
		{
			return new OneTimeEnumerator<T>(inner);
		}
	}
}