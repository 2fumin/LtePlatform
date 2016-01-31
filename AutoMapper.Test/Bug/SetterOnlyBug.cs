﻿using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
    namespace SetterOnlyBug
    {
        [TestFixture]
        public class MappingTests : AutoMapperSpecBase
        {
            protected override void Establish_context()
            {
                Mapper
                    .CreateMap<Source, Desitination>()
                    .ForMember("Property", o => o.Ignore());
            }

            [Test]
            public void TestMapping()
            {
                var source = new Source
                {
                    Property = "Something"
                };
                var destination = Mapper.Map<Source, Desitination>(source);

                destination.GetProperty().ShouldBeNull();
            }
        }

        public class Source
        {
            public string Property { get; set; }
        }

        public class Desitination
        {
            string _property;

            public string Property
            {
                set { _property = value; }
            }

            public string GetProperty()
            {
                return _property;
            }
        }
    }
}