using System;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class ConstructorParameterNamedType
    {
        public class SourceClass { }

        public class DestinationClass
        {
            public DestinationClass() { }

            public DestinationClass(int type)
            {
                Type = type;
            }

            public int Type { get; private set; }
        }

        [Test]
        public void Should_handle_constructor_parameter_named_type()
        {
            Mapper.Initialize(c => c.CreateMap<SourceClass, DestinationClass>());
            new Action(Mapper.AssertConfigurationIsValid).ShouldThrow<AutoMapperConfigurationException>(
                ex => ex.Errors[0].UnmappedPropertyNames[0].ShouldBe("Type"));
        }
    }
}