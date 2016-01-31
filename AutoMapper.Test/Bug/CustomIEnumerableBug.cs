using System.Collections.Generic;
using System.Linq;
using AutoMapper.Mappers;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
	public class One
	{
		public IEnumerable<string> Stuff { get; set; }
	}

	public class Two
	{
		public IEnumerable<Item> Stuff { get; set; }
	}

	public class Item
	{
		public string Value { get; set; }
	}

	public class StringToItemConverter : TypeConverter<IEnumerable<string>, IEnumerable<Item>>
	{
		protected override IEnumerable<Item> ConvertCore(IEnumerable<string> source)
		{
		    return (from s in source where !string.IsNullOrEmpty(s) select new Item {Value = s}).ToList();
		}
	}

    [TestFixture]
	public class AutoMapperBugTest
	{
		[Test]
		public void ShouldMapOneToTwo()
		{
            var config = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
			config.CreateMap<One, Two>();

			config.CreateMap<IEnumerable<string>, IEnumerable<Item>>().ConvertUsing<StringToItemConverter>();

			config.AssertConfigurationIsValid();

			var engine = new MappingEngine(config);
			var one = new One
			{
				Stuff = new List<string> { "hi", "", "mom" }
			};

			var two = engine.Map<One, Two>(one);

			two.ShouldNotBeNull();
			two.Stuff.Count().ShouldEqual(2);
		}
	}
}
