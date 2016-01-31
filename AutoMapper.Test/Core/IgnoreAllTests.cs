using System.Collections.Generic;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Core
{
    [TestFixture]
    public class IgnoreAllTests
    {
        public class Source
        {
            public string ShouldBeMapped { get; set; }
        }

        public class Destination
        {
            public string ShouldBeMapped { get; set; }
            public string StartingWith_ShouldNotBeMapped { get; set; }
            public List<string> StartingWith_ShouldBeNullAfterwards { get; set; }
            public string AnotherString_ShouldBeNullAfterwards { get; set; }
        }

        public class DestinationWrongType
        {
            public Destination ShouldBeMapped { get; set; }
        }

        [Test]
        public void GlobalIgnore_ignores_all_properties_beginning_with_string()
        {
			Mapper.Initialize(cfg =>
			{
				cfg.AddGlobalIgnore("StartingWith");
				cfg.CreateMap<Source, Destination>()
					.ForMember(dest => dest.AnotherString_ShouldBeNullAfterwards, opt => opt.Ignore());
			});
            
            Mapper.Map<Source, Destination>(new Source{ShouldBeMapped = "true"});
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void GlobalIgnore_ignores_properties_with_names_matching_but_different_types()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddGlobalIgnore("ShouldBeMapped");
                cfg.CreateMap<Source, DestinationWrongType>();
            });

            Mapper.Map<Source, DestinationWrongType>(new Source { ShouldBeMapped = "true" });
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void Ignored_properties_should_be_default_value()
        {
			Mapper.Initialize(cfg =>
			{
				cfg.AddGlobalIgnore("StartingWith");
				cfg.CreateMap<Source, Destination>()
					.ForMember(dest => dest.AnotherString_ShouldBeNullAfterwards, opt => opt.Ignore());
			});

            Destination destination = Mapper.Map<Source, Destination>(new Source { ShouldBeMapped = "true" });
            destination.StartingWith_ShouldBeNullAfterwards.ShouldBe(null);
            destination.StartingWith_ShouldNotBeMapped.ShouldBe(null);
        }

        [Test]
        public void Ignore_supports_two_different_values()
        {
			Mapper.Initialize(cfg =>
			{
				cfg.AddGlobalIgnore("StartingWith");
				cfg.AddGlobalIgnore("AnotherString");
				cfg.CreateMap<Source, Destination>();
			});

            Destination destination = Mapper.Map<Source, Destination>(new Source { ShouldBeMapped = "true" });
            destination.AnotherString_ShouldBeNullAfterwards.ShouldBe(null);
            destination.StartingWith_ShouldNotBeMapped.ShouldBe(null);
        }
    }

    [TestFixture]
	public class IgnoreAttributeTests
	{
		public class Source
		{
			public string ShouldBeMapped { get; set; }
			public string ShouldNotBeMapped { get; set; }
		}

		public class Destination
		{
			public string ShouldBeMapped { get; set; }
			[IgnoreMap]
			public string ShouldNotBeMapped { get; set; }
		}

		[Test]
		public void Ignore_On_Source_Field()
		{
			Mapper.CreateMap<Source, Destination>();
			Mapper.AssertConfigurationIsValid();

			Source source = new Source
			{
				ShouldBeMapped = "Value1",
				ShouldNotBeMapped = "Value2"
			};

			Destination destination = Mapper.Map<Source, Destination>(source);
            destination.ShouldNotBeMapped.ShouldBe(null);
		}
	}

    [TestFixture]
    public class ReverseMapIgnoreAttributeTests
    {
        public class Source
        {
            public string ShouldBeMapped { get; set; }
            public string ShouldNotBeMapped { get; set; }
        }

        public class Destination
        {
            public string ShouldBeMapped { get; set; }
            [IgnoreMap]
            public string ShouldNotBeMapped { get; set; }
        }

        [Test]
        public void Ignore_On_Source_Field()
        {
            Mapper.CreateMap<Source, Destination>()
                .ReverseMap();
            Mapper.AssertConfigurationIsValid();

            Destination source = new Destination
            {
                ShouldBeMapped = "Value1",
                ShouldNotBeMapped = "Value2"
            };

            Source destination = Mapper.Map<Destination, Source>(source);
            destination.ShouldNotBeMapped.ShouldBe(null);

        }
    }
}