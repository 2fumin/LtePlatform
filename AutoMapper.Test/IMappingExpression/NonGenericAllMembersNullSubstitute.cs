using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.IMappingExpression
{
    [TestFixture]
	public class NonGenericAllMembersNullSubstituteBug : AutoMapperSpecBase
	{
        public class Source
        {
            public int? Value1 { get; set; }

            public int? Value2 { get; set; }

            public int? Value3 { get; set; }
        }

        public class Destination
        {
            public string Value1 { get; set; }

            public string Value2 { get; set; }

            public string Value3 { get; set; }
        }

		[Test]
		public void Should_map_all_null_values_to_its_substitute()
		{
            Mapper.CreateMap(typeof(Source), typeof(Destination))
                .ForAllMembers(opt => opt.NullSubstitute(string.Empty));
		    Mapper.CreateMap<Source, Destination>()
		        .ForMember(d => d.Value1, opt => opt.MapFrom(s => s.Value1.ToString()))
                .ForMember(d => d.Value2, opt => opt.MapFrom(s => s.Value2.ToString()))
                .ForMember(d => d.Value3, opt => opt.MapFrom(s => s.Value3.ToString()));

		    var src = new Source
		    {
		        Value1 = 5
		    };

		    var dest = Mapper.Map<Source, Destination>(src);

		    dest.Value1.ShouldBe("5");
		    dest.Value2.ShouldBe(string.Empty);
		    dest.Value3.ShouldBe(string.Empty);
		}
	}
}
