using System.Linq;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.IMappingExpression
{
    namespace NonGenericReverseMapping
    {
        [TestFixture]
        public class When_reverse_mapping_classes_with_simple_properties : AutoMapperSpecBase
        {
            private Source _source;

            public class Source
            {
                public int Value { get; set; }
            }
            public class Destination
            {
                public int Value { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap(typeof(Source), typeof(Destination)).ReverseMap();
                });
            }

            protected override void Because_of()
            {
                var dest = new Destination
                {
                    Value = 10
                };
                _source = Mapper.Map<Destination, Source>(dest);
            }

            [Test]
            public void Should_create_a_map_with_the_reverse_items()
            {
                _source.Value.ShouldBe(10);
            }
        }

        [TestFixture]
        public class When_reverse_mapping_and_ignoring_via_method : NonValidatingSpecBase
        {
            public class Source
            {
                public int Value { get; set; }
            }

            public class Dest
            {
                public int Value { get; set; }
                public int Ignored { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap(typeof(Source), typeof(Dest))
                        .ForMember("Ignored", opt => opt.Ignore())
                        .ReverseMap();
                });
            }

            [Test]
            public void Should_show_valid()
            {
                typeof(AutoMapperConfigurationException).ShouldNotBeThrownBy(() => Mapper.AssertConfigurationIsValid());
            }
        }

        [TestFixture]
        public class When_reverse_mapping_and_ignoring : AutoMapperSpecBase
        {
            public class Foo
            {
                public string Bar { get; set; }
                public string Baz { get; set; }
            }

            public class Foo2
            {
                public string Bar { get; set; }
                public string Boo { get; set; }
            }

            [Test]
            public void GetUnmappedPropertyNames_ShouldReturnBoo()
            {
                //Arrange
                Mapper.CreateMap(typeof(Foo), typeof(Foo2));
                var typeMap = Mapper.GetAllTypeMaps()
                          .First(x => x.SourceType == typeof(Foo) && x.DestinationType == typeof(Foo2));
                //Act
                var unmappedPropertyNames = typeMap.GetUnmappedPropertyNames();
                //Assert
                unmappedPropertyNames[0].ShouldBe("Boo");
            }

            [Test]
            public void WhenSecondCallTo_GetUnmappedPropertyNames_ShouldReturnBoo()
            {
                Mapper.Reset();
                //Arrange
                Mapper.CreateMap(typeof(Foo), typeof(Foo2)).ReverseMap();
                var typeMap = Mapper.GetAllTypeMaps()
                          .First(x => x.SourceType == typeof(Foo2) && x.DestinationType == typeof(Foo));
                //Act
                var unmappedPropertyNames = typeMap.GetUnmappedPropertyNames();
                Assert.AreEqual(unmappedPropertyNames.Length, 1);
                Assert.IsNotNull(unmappedPropertyNames[0]);
                //Assert
                unmappedPropertyNames[0].ShouldBe("Boo");
            }

            [Test]
            public void Should_not_throw_exception_for_unmapped_properties()
            {
                Mapper.CreateMap(typeof(Foo), typeof(Foo2))
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExistingSource();

                Mapper.AssertConfigurationIsValid();
            }
        }

        public static class AutoMapperExtensions
        {
            // from http://stackoverflow.com/questions/954480/automapper-ignore-the-rest/6474397#6474397
            public static AutoMapper.IMappingExpression IgnoreAllNonExisting(this AutoMapper.IMappingExpression expression)
            {
                var sourceType = expression.TypeMap.SourceType;
                var destinationType = expression.TypeMap.DestinationType;
                var existingMaps = AutoMapper.Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
                foreach(var property in existingMaps.GetUnmappedPropertyNames())
                {
                    expression.ForMember(property, opt => opt.Ignore());
                }
                return expression;
            }

            public static AutoMapper.IMappingExpression IgnoreAllNonExistingSource(this AutoMapper.IMappingExpression expression)
            {
                var sourceType = expression.TypeMap.SourceType;
                var destinationType = expression.TypeMap.DestinationType;
                var existingMaps = AutoMapper.Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
                foreach(var property in existingMaps.GetUnmappedPropertyNames())
                {
                    expression.ForSourceMember(property, opt => opt.Ignore());
                }
                return expression;
            }
        }
    }
}