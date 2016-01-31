using System;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class NullableDateTime : AutoMapperSpecBase
    {
        Destination _destination;
        readonly DateTime _date = new DateTime(1900, 1, 1);

        public class Source
        {
            public DateTime Value { get; set; }
        }

        public class Destination
        {
            public DateTime Value { get; set; }
        }

        protected override void Establish_context()
        {
            Mapper.Initialize(c=>
            {
                c.CreateMap<Source, Destination>();
                c.CreateMap<DateTime, DateTime?>().ConvertUsing(source => source == new DateTime(1900, 1, 1) ? (DateTime?)null : source);
            });
        }
        
        [Test]
        public void Should_map_as_usual()
        {
            _destination = Mapper.Map<Destination>(new Source { Value = _date });
            _destination.Value.ShouldBe(_date);
        }
    }
}