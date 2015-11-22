using System.Collections.Generic;
using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class TargetISet : AutoMapperSpecBase
    {
        Destination _destination;
        readonly string[] _items = { "one", "two", "three" };

        public class Source
        {
            public IEnumerable<string> Items { get; set; }
        }

        class Destination
        {
            public ISet<string> Items { get; set; }
        }

        protected override void Establish_context()
        {
            Mapper.CreateMap<Source, Destination>();
        }

        protected override void Because_of()
        {
            _destination = Mapper.Map<Destination>(new Source { Items = _items });
        }

        [Test]
        public void Should_map_IEnumerable_to_ISet()
        {
            _destination.Items.SetEquals(_items).ShouldBeTrue();
        }
    }
}