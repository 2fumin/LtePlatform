using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
	public abstract class When_mapping_for_derived_class_is_duplicated : AutoMapperSpecBase
	{
		public class ModelObject
		{
			public string BaseString { get; set; }
		}

		public abstract class ModelSubObject : ModelObject
		{
			public string SubString { get; set; }
		}

		public class DtoObject
		{
			public string BaseString { get; set; }
		}

		public abstract class DtoSubObject : DtoObject
		{
			public string SubString { get; set; }
		}

		[Test]
		public void should_not_throw_duplicated_key_exception()
		{
			Mapper.CreateMap<ModelSubObject, DtoObject>()
				.Include<ModelSubObject, DtoSubObject>();

			Mapper.CreateMap<ModelSubObject, DtoSubObject>();

			Mapper.CreateMap<ModelSubObject, DtoObject>()
				.Include<ModelSubObject, DtoSubObject>();

			Mapper.CreateMap<ModelSubObject, DtoSubObject>();
		}
	}
}
