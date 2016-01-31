using System.CodeDom;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Core
{
	namespace DynamicMapping
	{
        [TestFixture]
		public class When_mapping_two_non_configured_types : AutoMapperSpecBase
		{
			private Destination _resultWithGenerics;
			private Destination _resultWithoutGenerics;

			public class Source
			{
				public int Value { get; set; }
			}

			public class Destination
			{
				public int Value { get; set; }
			}

			protected override void Because_of()
			{
				_resultWithGenerics = Mapper.DynamicMap<Source, Destination>(new Source {Value = 5});
				_resultWithoutGenerics = (Destination) Mapper.DynamicMap(new Source {Value = 5}, typeof(Source), typeof(Destination));
			}

			[Test]
			public void Should_dynamically_map_the_two_types()
			{
				_resultWithGenerics.Value.ShouldBe(5);
				_resultWithoutGenerics.Value.ShouldBe(5);
			}
		}

        [TestFixture]
        public class When_mapping_two_non_configured_types_with_nesting : NonValidatingSpecBase
        {
            private Destination _resultWithGenerics;

            public class Source
            {
                public int Value { get; set; }
                public ChildSource Child { get; set; }
            }

            public class ChildSource
            {
                public string Value2 { get; set; }
            }

            public class Destination
            {
                public int Value { get; set; }
                public ChildDestination Child { get; set; }
            }

            public class ChildDestination
            {
                public string Value2 { get; set; }
            }

            protected override void Because_of()
            {
                var source = new Source
                {
                    Value = 5,
                    Child = new ChildSource
                    {
                        Value2 = "foo"
                    }
                };
                _resultWithGenerics = Mapper.DynamicMap<Source, Destination>(source);
            }

            [Test]
            public void Should_dynamically_map_the_two_types()
            {
                _resultWithGenerics.Value.ShouldBe(5);
            }

            [Test]
            public void Should_dynamically_map_the_children()
            {
                _resultWithGenerics.Child.Value2.ShouldBe("foo");
            }
        }

        [TestFixture]
		public class When_mapping_two_non_configured_types_that_do_not_match : NonValidatingSpecBase
		{
			public class Source
			{
				public int Value { get; set; }
			}

			public class Destination
			{
				public int Valuefff { get; set; }
			}

			[Test]
			public void Should_ignore_any_members_that_do_not_match()
			{
				var destination = Mapper.DynamicMap<Source, Destination>(new Source {Value = 5});

				destination.Valuefff.ShouldBe(0);
			}

			[Test]
			public void Should_not_throw_any_configuration_errors()
			{
				typeof(AutoMapperConfigurationException).ShouldNotBeThrownBy(() => Mapper.DynamicMap<Source, Destination>(new Source { Value = 5 }));
			}
		}

        [TestFixture]
		public class When_mapping_to_an_existing_destination_object : NonValidatingSpecBase
		{
			private Destination _destination;

			public class Source
			{
				public int Value { get; set; }
				public int Value2 { get; set; }
			}

			public class Destination
			{
				public int Valuefff { get; set; }
				public int Value2 { get; set; }
			}

			protected override void Because_of()
			{
				_destination = new Destination { Valuefff = 7};
				Mapper.DynamicMap(new Source { Value = 5, Value2 = 3}, _destination);
			}

			[Test]
			public void Should_preserve_existing_values()
			{
				_destination.Valuefff.ShouldBe(7);
			}

			[Test]
			public void Should_map_new_values()
			{
				_destination.Value2.ShouldBe(3);
			}
		}

        [TestFixture]
		public class When_mapping_from_an_anonymous_type_to_an_interface : SpecBase
		{
			private IDestination _result;

			public interface IDestination
			{
				int Value { get; set; }
			}
            
			[Test]
			public void Should_allow_dynamic_mapping()
			{
				_result = Mapper.DynamicMap<IDestination>(new {Value = 5});
				_result.Value.ShouldBe(5);
			}
		}
	}
}