using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class InterfaceSelfMappingBug
    {
        public interface IFoo
        {
            int Value { get; set; } 
        }

        public class Bar : IFoo
        {
            public int Value { get; set; }
        }

        public class Baz : IFoo
        {
            public int Value { get; set; }
        }

        [Test]
        public void Example()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.CreateMap<IFoo, IFoo>();
            });
            Mapper.AssertConfigurationIsValid();

            IFoo bar = new Bar
            {
                Value = 5
            };
            IFoo baz = new Baz
            {
                Value = 10
            };

            Mapper.Map(bar, baz);

            baz.Value.ShouldEqual(5);
        }
    }
}