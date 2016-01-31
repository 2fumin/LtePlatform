using System.Collections.Generic;
using System.Linq;
using Shouldly;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class CollectionWhere : AutoMapperSpecBase
    {
        private Destination _destination;
        private readonly List<int> _sourceList = new List<int>() { 1, 2, 3 };

        class Source
        {
            public int Id { get; set; }

            public IEnumerable<int> ListProperty { get; set; }
        }

        class Destination
        {
            public int Id { get; set; }

            public IEnumerable<int> ListProperty { get; set; }
        }

        protected override void Establish_context()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source, Destination>();
            });
        }

        protected override void Because_of()
        {
            var source = new Source()
            {
                Id = 1,
                ListProperty = _sourceList,
            };
            _destination = new Destination()
            {
                Id = 2,
                ListProperty = new List<int>() { 4, 5, 6 }.Where(a=>true)
            };
            _destination = Mapper.Map(source, _destination);
        }

        [Test]
        public void Should_map_collections_with_where()
        {
            _destination.ListProperty.SequenceEqual(_sourceList).ShouldBeTrue();
        }
    }
}