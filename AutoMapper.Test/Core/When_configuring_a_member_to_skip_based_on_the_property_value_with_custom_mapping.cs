using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Core.ConditionalMapping
{
    [TestFixture]
    public class When_configuring_a_member_to_skip_based_on_the_property_value_with_custom_mapping : AutoMapperSpecBase
    {
        public class Source
        {
            public int Value { get; set; }
        }

        public class Destination
        {
            public int Value { get; set; }
        }

        protected override void Establish_context()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source, Destination>()
                    .ForMember(dest => dest.Value, opt =>
                    {
                        opt.Condition(src => src.Value > 0);
                        opt.ResolveUsing((Source src) => 10);
                    });
            });
        }

        [Test]
        public void Should_skip_the_mapping_when_the_condition_is_true()
        {
            var destination = Mapper.Map<Source, Destination>(new Source { Value = -1 });

            destination.Value.ShouldBe(0);
        }

        [Test]
        public void Should_execute_the_mapping_when_the_condition_is_false()
        {
            Mapper.Map<Source, Destination>(new Source { Value = 7 }).Value.ShouldBe(10);
        }
    }
}