using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class CollectionsNullability : AutoMapperSpecBase
    {
        Holder _destination;

        public class Container
        {
            public List<string> Items { get; set; }
        }

        class Holder
        {
            public Container[] Containers { get; set; }
        }

        protected override void Establish_context()
        {
            Mapper.CreateMap<Holder, Holder>();
            Mapper.CreateMap<Container, Container>();
        }

        protected override void Because_of()
        {
            var from = new Holder { Containers = new[] { new Container() } };
            _destination = Mapper.Map<Holder>(from);
        }

        [Test]
        public void Should_map_null_collection_to_not_null()
        {
            _destination.Containers[0].Items.ShouldNotBeNull();
        }
    }
}