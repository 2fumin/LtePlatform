using System;
using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class NonExistingProperty : AutoMapperSpecBase
    {
        public class Source
        {
        }

        public class Destination
        {
        }

        [Test]
        public void Should_report_missing_property()
        {
            var mapping = Mapper.CreateMap<Source, Destination>();
            new Action(() => mapping.ForMember("X", s => { })).ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}
