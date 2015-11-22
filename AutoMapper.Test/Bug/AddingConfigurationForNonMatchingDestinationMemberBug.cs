using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class AddingConfigurationForNonMatchingDestinationMemberBug
    {
        public class Source
        {

        }

        public class Destination
        {
            public string Value { get; set; }
        }

        [SetUp]
        public void Establish_context()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source, Destination>()
                    .ForMember(dest => dest.Value, opt => opt.NullSubstitute("Foo"));
            });
        }

        [Test]
        [ExpectedException(typeof(AutoMapperConfigurationException))]
        public void Should_show_configuration_error()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}