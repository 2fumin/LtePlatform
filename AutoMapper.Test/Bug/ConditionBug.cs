﻿using System;
using System.Collections.Generic;
using Shouldly;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    namespace ConditionBug
    {
        [TestFixture]
        public class Example : AutoMapperSpecBase
        {
            public class SubSource
            {
                public string SubValue { get; set; }
            }

            public class Source
            {
                public Source()
                {
                    Value = new List<SubSource>();
                }

                public List<SubSource> Value { get; set; }
            }

            public class Destination
            {
                public string Value { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Source, Destination>()
                                             .ForMember(dest => dest.Value, opt =>
                                             {
                                                 opt.Condition(src => src.Value.Count > 1);
                                                 opt.ResolveUsing(src => src.Value[1].SubValue);
                                             }));
            }

            [Test]
            public void Should_skip_the_mapping_when_the_condition_is_false()
            {
                var src = new Source();
                src.Value.Add(new SubSource {SubValue = "x"});
                var destination = Mapper.Map<Source, Destination>(src);

                destination.Value.ShouldBeNull();
            }

            [Test]
            public void Should_execute_the_mapping_when_the_condition_is_true()
            {
                var src = new Source();
                src.Value.Add(new SubSource {SubValue = "x"});
                src.Value.Add(new SubSource {SubValue = "x"});
                var destination = Mapper.Map<Source, Destination>(src);

                destination.Value.ShouldBe("x");
            }
        }

        [TestFixture]
        public class PrimitiveExample : AutoMapperSpecBase
        {
            public class Source
            {
                public int? Value { get; set; }
            }

            public class Destination
            {
                public int Value { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Source, Destination>()
                    .ForMember(d => d.Value, opt =>
                    {
                        opt.Condition(src => src.Value.HasValue);
                        opt.MapFrom(src => src.Value.Value + 10);
                    }));
            }


            [Test]
            public void Should_skip_when_condition_not_met()
            {
                var dest = Mapper.Map<Source, Destination>(new Source());

                dest.Value.ShouldBe(0);
            }
        }
    }

    namespace ConditionPropertyBug
    {
        [TestFixture]
        public class Example : AutoMapperSpecBase
        {
            public class Source
            {
                private int basePrice;
                public bool HasBasePrice { get; set; }
                public int BasePrice
                {
                    get
                    {
                        if (!HasBasePrice)
                            throw new InvalidOperationException("Has no base price");

                        return basePrice;
                    }
                    set
                    {
                        basePrice = value;
                        HasBasePrice = true;
                    }
                }
            }

            public class Destination
            {
                public int BasePrice { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Source, Destination>()
                    .ForMember(itemDTO => itemDTO.BasePrice,
                        config =>
                        {
                            config.Condition(item => item.HasBasePrice);
                            config.MapFrom(item => item.BasePrice);
                        }));
            }

            [Test]
            public void Should_skip_the_mapping_when_the_condition_property_is_false()
            {
                var src = new Source();
                var dest = Mapper.Map<Source, Destination>(src);

                dest.BasePrice.ShouldBe(0);
            }

            [Test]
            public void Should_execute_the_mapping_when_the_condition_property_is_true()
            {
                var src = new Source {BasePrice = 15};
                var dest = Mapper.Map<Source, Destination>(src);

                dest.BasePrice.ShouldBe(src.BasePrice);
            }
        }
    }


    namespace SourceValueConditionPropertyBug
    {
        public class Source
        {
            public int Value { get; set; }
        }

        public class Dest
        {
            public int? Value { get; set; }
        }

        [TestFixture]
        public class ConditionTests : AutoMapperSpecBase
        {
            protected override void Establish_context()
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Source, Dest>()
                    .ForMember(d => d.Value, opt => opt.Condition(rc => rc.DestinationValue == null)));
            }

            [Test]
            public void Should_map_value_when_null()
            {
                var destination = new Dest();
                Mapper.Map(new Source {Value = 5}, destination);
                destination.Value.ShouldBe(5);
            }

            [Test]
            public void Should_not_map_value_when_not_null()
            {
                var destination = new Dest { Value = 6};
                Mapper.Map(new Source {Value = 5}, destination);
                destination.Value.ShouldBe(6);
            }
        }
    }

    namespace SourceValueExceptionConditionPropertyBug
    {
        public class Source
        {
            public bool Accessed = false;
            public int Value
            {
                get
                {
                    Accessed = true;
                    return 5;
                }
            }
        }

        public class Dest
        {
            public int Value { get; set; }
        }

        [TestFixture]
        public class ConditionTests : NonValidatingSpecBase
        {
            protected override void Establish_context()
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Source, Dest>()
                    .ForMember(d => d.Value, opt => opt.PreCondition((ResolutionContext rc) => false)));
            }

            [Test]
            public void Should_not_map()
            {
                var source = new Source();
                Mapper.Map<Source, Dest>(source);
                source.Accessed.ShouldBeFalse();
            }
        }
    }

}
