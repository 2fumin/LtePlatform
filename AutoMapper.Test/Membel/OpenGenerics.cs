using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Membel
{
    [TestFixture]
    public class OpenGenerics
    {
        public class Source<T>
        {
            public int A { get; set; }
            public T Value { get; set; }
        }

        public class Dest<T>
        {
            public int A { get; set; }
            public T Value { get; set; }
        }

        [Test]
        public void Can_map_simple_generic_types()
        {
            Mapper.Initialize(cfg => cfg.CreateMap(typeof(Source<>), typeof(Dest<>)));

            var source = new Source<int>
            {
                Value = 5
            };

            var dest = Mapper.Map<Source<int>, Dest<int>>(source);

            dest.Value.ShouldBe(5);
        }

        [Test]
        public void Can_map_non_generic_members()
        {
            Mapper.Initialize(cfg => cfg.CreateMap(typeof(Source<>), typeof(Dest<>)));

            var source = new Source<int>
            {
                A = 5
            };

            var dest = Mapper.Map<Source<int>, Dest<int>>(source);

            dest.A.ShouldBe(5);
        }

        [Test]
        public void Can_map_recursive_generic_types()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source<int>, Dest<double>>()
                    .ForMember(d => d.Value, opt => opt.MapFrom(s => (double) s.Value));
                cfg.CreateMap(typeof (Source<>), typeof (Dest<>));
            });

            var source = new Source<Source<int>>
            {
                Value = new Source<int>
                {
                    Value = 5,
                }
            };

            var dest = Mapper.Map<Source<Source<int>>, Dest<Dest<double>>>(source);

            dest.Value.Value.ShouldBe(5);
        }
    }
}