using System;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class PropertyNamedType
    {
        class Source
        {
            public int Number { get; set; }
        }
        class Destination
        {
            public int Type { get; set; }
        }

        [Test]
        public void Should_detect_unmapped_destination_property_named_type()
        {
            Mapper.Initialize(c=>c.CreateMap<Source, Destination>());
            new Action(Mapper.AssertConfigurationIsValid).ShouldThrow<AutoMapperConfigurationException>(
                ex=>ex.Errors[0].UnmappedPropertyNames[0].ShouldBe("Type"));
        }
    }
}