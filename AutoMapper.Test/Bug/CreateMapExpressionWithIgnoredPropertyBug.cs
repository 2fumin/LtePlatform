using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class CreateMapExpressionWithIgnoredPropertyBug : NonValidatingSpecBase
    {
        [Test]
        public void ShouldNotMapPropertyWhenItIsIgnored()
        {
            Mapper.CreateMap<Person, Person>()
                .ForMember(x => x.Name, x => x.Ignore());

            var collection = (new List<Person> { new Person { Name = "Person1" } }).AsQueryable();

            var result = collection.ProjectTo<Person>().ToList();

            result.ForEach(x => x.Name.ShouldBeNull());
        }

        public class Person
        {
            public string Name { get; set; }
        }
    }
}