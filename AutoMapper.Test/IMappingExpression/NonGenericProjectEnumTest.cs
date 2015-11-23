using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper.Should;
using AutoMapper.Should.Core.Assertions;
using NUnit.Framework;

namespace AutoMapper.Test.IMappingExpression
{
    [TestFixture]
    public class NonGenericProjectEnumTest
    {
        public NonGenericProjectEnumTest()
        {
            Mapper.CreateMap(typeof(Customer), typeof(CustomerDto));
            Mapper.CreateMap(typeof(CustomerType), typeof(string)).ProjectUsing(ct => ct.ToString().ToUpper());
        }

        [Test]
        public void ProjectingEnumToString()
        {
            var customers = new[] { new Customer() { FirstName = "Bill", LastName = "White", CustomerType = CustomerType.Vip } }.AsQueryable();

            var projected = customers.ProjectTo<CustomerDto>();
            projected.ShouldNotBeNull();
            CustomizeAssert.Equal(customers.Single().CustomerType.ToString().ToUpper(), projected.Single().CustomerType);
        }

        public class Customer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public CustomerType CustomerType { get; set; }
        }

        public class CustomerDto
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string CustomerType { get; set; }
        }

        public enum CustomerType
        {
            Regular,
            Vip,
        }
    }

    [TestFixture]
    public class NonGenericProjectAndMapEnumTest
    {
        public NonGenericProjectAndMapEnumTest()
        {
            Mapper.CreateMap(typeof(Customer), typeof(CustomerDto));
            Mapper.CreateMap(typeof(CustomerType), typeof(string)).ProjectUsing(ct => ct.ToString().ToUpper());
        }

        [Test]
        public void ProjectingEnumToString()
        {
            var customers = new[] { new Customer() { FirstName = "Bill", LastName = "White", CustomerType = CustomerType.Vip } }.AsQueryable();

            var projected = Mapper.Map<CustomerDto[]>(customers);
            projected.ShouldNotBeNull();
            CustomizeAssert.Equal(customers.Single().CustomerType.ToString().ToUpper(), projected.Single().CustomerType);
        }

        public class Customer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public CustomerType CustomerType { get; set; }
        }

        public class CustomerDto
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string CustomerType { get; set; }
        }

        public enum CustomerType
        {
            Regular,
            Vip,
        }
    }

    [TestFixture]
    public class NonGenericProjectionOverrides : NonValidatingSpecBase
    {
        public class Source
        {
            
        }

        public class Dest
        {
            public int Value { get; set; }
        }

        protected override void Establish_context()
        {
            Mapper.CreateMap(typeof(Source), typeof(Dest)).ProjectUsing(src => new Dest {Value = 10});
        }

        [Test]
        public void Should_validate_because_of_overridden_projection()
        {
            typeof(AutoMapperConfigurationException).ShouldNotBeThrownBy(Mapper.AssertConfigurationIsValid);
        }
    }
}
