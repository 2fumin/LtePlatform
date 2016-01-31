using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class ProjectConstructorParameters : AutoMapperSpecBase
    {
        SourceDto _dest;
        const int SomeValue = 15;

        public class Inner
        {
            public int Member { get; set; }
        }

        public class Source
        {
            public int Value { get; set; }
            public Inner Inner { get; set; }
        }

        public class SourceDto
        {
            public SourceDto(int innerMember)
            {
                Value = innerMember;
            }

            public int Value { get; }
        }

        protected override void Establish_context()
        {
            Mapper.CreateMap<Source, SourceDto>();
        }

        protected override void Because_of()
        {
            var source = new Source { Inner = new Inner { Member = SomeValue } };
            //_dest = Mapper.Map<Source, SourceDto>(source);
            _dest = new[] { source }.AsQueryable().ProjectTo<SourceDto>().First();
        }

        [Test]
        public void Should_project_constructor_parameter_mappings()
        {
            _dest.Value.ShouldBe(SomeValue);
        }
    }
}