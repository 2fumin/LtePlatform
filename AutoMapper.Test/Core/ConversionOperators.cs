using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Core
{
    namespace ConversionOperators
    {
        [TestFixture]
        public class When_mapping_to_classes_with_implicit_conversion_operators_on_the_destination : AutoMapperSpecBase
        {
            private Bar _bar;

            public class Foo
            {
                public string Value { get; set; }
            }

            public class Bar
            {
                public string OtherValue { get; set; }

                public static implicit operator Bar(Foo other)
                {
                    return new Bar
                    {
                        OtherValue = other.Value
                    };
                }

            }

            protected override void Because_of()
            {
                var source = new Foo { Value = "Hello" };

                _bar = Mapper.Map<Foo, Bar>(source);
            }

            [Test]
            public void Should_use_the_implicit_conversion_operator()
            {
                _bar.OtherValue.ShouldBe("Hello");
            }
        }
        
        [TestFixture]
        public class When_mapping_to_classes_with_implicit_conversion_operators_on_the_source : AutoMapperSpecBase
        {
            private Bar _bar;

            public class Foo
            {
                public string Value { get; set; }

                public static implicit operator Bar(Foo other)
                {
                    return new Bar
                    {
                        OtherValue = other.Value
                    };
                }

                public static implicit operator string(Foo other)
                {
                    return other.Value;
                }

            }

            public class Bar
            {
                public string OtherValue { get; set; }
            }

            protected override void Because_of()
            {
                var source = new Foo { Value = "Hello" };

                _bar = Mapper.Map<Foo, Bar>(source);
            }

            [Test]
            public void Should_use_the_implicit_conversion_operator()
            {
                _bar.OtherValue.ShouldBe("Hello");
            }
        }

        [TestFixture]
        public class When_mapping_to_classes_with_explicit_conversion_operator_on_the_destination : AutoMapperSpecBase
        {
            private Bar _bar;

            public class Foo
            {
                public string Value { get; set; }
            }

            public class Bar
            {
                public string OtherValue { get; set; }

                public static explicit operator Bar(Foo other)
                {
                    return new Bar
                    {
                        OtherValue = other.Value
                    };
                }
            }

            protected override void Because_of()
            {
                _bar = Mapper.Map<Foo, Bar>(new Foo { Value = "Hello" });
            }

            [Test]
            public void Should_use_the_explicit_conversion_operator()
            {
                _bar.OtherValue.ShouldBe("Hello");
            }
        }

        [TestFixture]
        public class When_mapping_to_classes_with_explicit_conversion_operator_on_the_source : AutoMapperSpecBase
        {
            private Bar _bar;

            public class Foo
            {
                public string Value { get; set; }

                public static explicit operator Bar(Foo other)
                {
                    return new Bar
                    {
                        OtherValue = other.Value
                    };
                }
            }

            public class Bar
            {
                public string OtherValue { get; set; }
            }

            protected override void Because_of()
            {
                _bar = Mapper.Map<Foo, Bar>(new Foo { Value = "Hello" });
            }

            [Test]
            public void Should_use_the_explicit_conversion_operator()
            {
                _bar.OtherValue.ShouldBe("Hello");
            }
        }
    }
}