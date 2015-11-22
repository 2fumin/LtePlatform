using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class CaseSensitivityBug : NonValidatingSpecBase
    {
        protected override void Establish_context()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Foo, Bar>());
        }

        [Test]
        public void TestMethod1()
        {
            typeof(AutoMapperConfigurationException).ShouldNotBeThrownBy(Mapper.AssertConfigurationIsValid);
        }

        public class Foo
        {
            public int ID { get; set; }
        }

        public class Bar
        {
            public int id { get; set; }
        }
    }
}