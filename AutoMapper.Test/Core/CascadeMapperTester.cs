using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Core
{
    [TestFixture]
    public class CascadeMapperTester : AutoMapperSpecBase
    {
        private Destination _destination;
        private Source _source;
        private CascadeDestination _cascadeDestination;

        public class Source
        {
             public Destination Destination { get; set; }
        }

        public class Destination
        {
            public int Foo1 { get; set; } 

            public int Foo2 { get; set; }
        }

        public class CascadeDestination
        {
            public Destination Destination { get; set; }
        }

        public class ExtendedDestination
        {
            public int Foo1 { get; set; }

            public int Foo2 { get; set; }

            public int Foo3 { get; set; }
        }

        public class ExtendedCascadeDestination
        {
            public ExtendedDestination ExtendedDestination { get; set; } 
        }

        protected override void Establish_context()
        {
            Mapper.CreateMap<Destination, Destination>();
            Mapper.CreateMap<Source, CascadeDestination>();
            Mapper.CreateMap<Destination, ExtendedDestination>();
            Mapper.CreateMap<Source, ExtendedCascadeDestination>()
                .ForMember(d => d.ExtendedDestination,
                    opt => opt.MapFrom(s => Mapper.Map<Destination, ExtendedDestination>(s.Destination)));
        }

        protected override void Because_of()
        {
            _source = new Source
            {
                Destination = new Destination
                {
                    Foo1 = 11,
                    Foo2 = 22
                }
            };
        }

        [Test]
        public void Test_plane_case()
        {
            _destination = Mapper.Map<Destination, Destination>(_source.Destination);
            _destination.Foo1.ShouldBe(11);
            _destination.Foo2.ShouldBe(22);
        }

        [Test]
        public void Test_cascade_case()
        {
            _cascadeDestination = Mapper.Map<Source, CascadeDestination>(_source);
            _cascadeDestination.Destination.Foo1.ShouldBe(11);
            _cascadeDestination.Destination.Foo2.ShouldBe(22);
        }

        [Test]
        public void Test_extended_cascade_case()
        {
            var extended = Mapper.Map<Source, ExtendedCascadeDestination>(_source);
            extended.ExtendedDestination.Foo1.ShouldBe(11);
            extended.ExtendedDestination.Foo2.ShouldBe(22);
        }

        [Test]
        public void Test_extended_cascade_case_list()
        {
            var sources = new List<Source>
            {
                _source
            };
            var extended = Mapper.Map<List<Source>, IEnumerable<ExtendedCascadeDestination>>(sources);
            extended.ElementAt(0).ExtendedDestination.Foo1.ShouldBe(11);
            extended.ElementAt(0).ExtendedDestination.Foo2.ShouldBe(22);
        }
    }
}
