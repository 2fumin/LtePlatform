using System.Collections;
using System.Collections.Generic;
using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
	public class SequenceContainsNoElementsTest : AutoMapperSpecBase
	{
        public SequenceContainsNoElementsTest()
        {
            SetUp();
        }

        public void SetUp()
		{
			Mapper.CreateMap<Person, PersonModel>();
		}

		[Test]
		public void should_not_throw_InvalidOperationException()
		{
			var personArr = new Person[] { };
			var people = new People(personArr);
			var pmc = Mapper.Map<People, List<PersonModel>>(people);
		    pmc.ShouldNotBeNull();
            pmc.Count.ShouldEqual(0);
		}
	}

	public class People : IEnumerable
	{
		private readonly Person[] people;
		public People(Person[] people)
		{
			this.people = people;
		}
		public IEnumerator GetEnumerator()
		{
		    return people.GetEnumerator();
		}
	}

	public abstract class Person
	{
		public string Name { get; set; }
	}

	public abstract class PersonModel
	{
		public string Name { get; set; }
	}
}
