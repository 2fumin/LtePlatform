using System;
using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public abstract class NullableDateTime : AutoMapperSpecBase
    {
        Destination _destination;
        readonly DateTime _date = new DateTime(1900, 1, 1);

        public class Source
        {
            public DateTime Value { get; set; }
        }

        public abstract class Destination
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

        protected override void Because_of()
        {
            _destination = Mapper.Map<Destination>(new Source { Value = _date });
        }

        [Test]
        public void Should_map_as_usual()
        {
            _destination.Value.ShouldEqual(_date);
        }
    }
}