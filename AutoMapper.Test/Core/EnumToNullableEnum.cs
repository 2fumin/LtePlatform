﻿using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Core
{
    [TestFixture]
    public class EnumToNullableEnum : AutoMapperSpecBase
    {
        Destination _destination;
        public enum SomeEnum { Foo, Bar }

        public class Source
        {
            public SomeEnum EnumValue { get; set; }
        }

        public class Destination
        {
            public SomeEnum? EnumValue { get; set; }
        }

        protected override void Establish_context()
        {
            Mapper.CreateMap<Source, Destination>();
            Mapper.AssertConfigurationIsValid();
        }

        protected override void Because_of()
        {
            _destination = Mapper.Map<Source, Destination>(new Source{ EnumValue = SomeEnum.Bar });
        }

        [Test]
        public void Should_map_enum_to_nullable_enum()
        {
            _destination.EnumValue.ShouldEqual(SomeEnum.Bar);
        }
    }
}