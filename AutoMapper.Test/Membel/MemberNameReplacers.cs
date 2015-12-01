using NUnit.Framework;

namespace AutoMapper.Test.Membel
{
    [TestFixture]
    public class When_using_a_member_name_replacer : AutoMapperSpecBase
    {
        public class Source
        {
            public int Value { get; set; }
            public int Ävíator { get; set; }
            public int SubAirlinaFlight { get; set; }
        }

        public class Destination
        {
            public int Value { get; set; }
            public int Aviator { get; set; }
            public int SubAirlineFlight { get; set; }
        }

        [Test]
        public void Should_map_properties_with_different_names()
        {
            Mapper.Initialize(c =>
            {
                c.ReplaceMemberName("Ä", "A");
                c.ReplaceMemberName("í", "i");
                c.ReplaceMemberName("Airlina", "Airline");
            });

            var source = new Source()
            {
                Value = 5,
                Ävíator = 3,
                SubAirlinaFlight = 4
            };

            Mapper.CreateMap<Source, Destination>();
            var destination = Mapper.Map<Source, Destination>(source);

            Assert.AreEqual(source.Value, destination.Value);
            Assert.AreEqual(source.Ävíator, destination.Aviator);
            Assert.AreEqual(source.SubAirlinaFlight, destination.SubAirlineFlight);
        }
    }

}
