using System;
using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Membel
{
	namespace Profiles
	{
        [TestFixture]
	    public class When_segregating_configuration_through_a_profile : NonValidatingSpecBase
		{
			private Dto _result;

			public class Model
			{
				public int Value { get; set; }
			}

			public class Dto
			{
			    public Dto(string value)
			    {
			        Value = value;
			    }

				public string Value { get; }
			}

			protected override void Establish_context()
			{
                Mapper.Initialize(cfg =>
                {
                    cfg.DisableConstructorMapping();

                    cfg.CreateProfile("Custom");

                    cfg.CreateMap<Model, Dto>().WithProfile("Custom");
                });
			}

			protected override void Because_of()
			{
				_result = Mapper.Map<Model, Dto>(new Model {Value = 5});
			}

			[Test]
			public void Should_not_include_default_profile_configuration_with_profiled_maps()
			{
				_result.Value.ShouldEqual("5");
			}
		}

        [TestFixture]
		public class When_configuring_a_profile_through_a_profile_subclass : AutoMapperSpecBase
		{
			private Dto _result;
		    private CustomProfile1 _customProfile;

		    public class Model
			{
				public int Value { get; set; }
			}

			public class Dto
			{
				public string FooValue { get; set; }
			}

			public class Dto2
			{
				public string FooValue { get; set; }
			}

			public class CustomProfile1 : Profile
			{
				protected override void Configure()
				{
                    RecognizeDestinationPrefixes("Foo");
					CreateMap<Model, Dto>();
				}
			}

			public class CustomProfile2 : Profile
			{
				protected override void Configure()
				{
                    RecognizeDestinationPrefixes("Foo");

                    CreateMap<Model, Dto2>();
				}
			}

			protected override void Establish_context()
			{
			    _customProfile = new CustomProfile1();
			    Mapper.AddProfile(_customProfile);
				Mapper.AddProfile<CustomProfile2>();
			}

			protected override void Because_of()
			{
				_result = Mapper.Map<Model, Dto>(new Model { Value = 5 });
			}

		    [Test]
		    public void Should_default_the_custom_profile_name_to_the_type_name()
		    {
                _customProfile.ProfileName.ShouldEqual(typeof(CustomProfile1).FullName);
		    }

			[Test]
			public void Should_use_the_overridden_configuration_method_to_configure()
			{
				_result.FooValue.ShouldEqual("5");
			}
		}

        [TestFixture]
        public class When_disabling_constructor_mapping_with_profiles : AutoMapperSpecBase
        {
            private B _b;

            public class AProfile : Profile
            {
                protected override void Configure()
                {
                    DisableConstructorMapping();
                    CreateMap<A, B>();
                }
            }

            public class A
            {
                public string Value { get; set; }
            }

            public class B
            {

                public B()
                {
                }

                public B(string value)
                {
                    throw new Exception();
                }

                public string Value { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.AddProfile<AProfile>();
            }

            protected override void Because_of()
            {
                _b = Mapper.Map<B>(new A { Value = "BLUEZ" });
            }

            [Test]
            public void When_using_profile_and_no_constructor_mapping()
            {
                Assert.AreEqual("BLUEZ", _b.Value);
            }
        }


	}
}
