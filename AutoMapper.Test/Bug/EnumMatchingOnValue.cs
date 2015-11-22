using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class EnumMatchingOnValue : AutoMapperSpecBase
    {
        private SecondClass _result;

        public class FirstClass
        {
            public FirstEnum EnumValue { get; set; }
        }

        public enum FirstEnum
        {
            NamedEnum = 1,
            SecondNameEnum = 2
        }

        public class SecondClass
        {
            public SecondEnum EnumValue { get; set; }
        }

        public enum SecondEnum
        {
            DifferentNamedEnum = 1,
            SecondNameEnum = 2
        }
        protected override void Establish_context()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<FirstClass, SecondClass>();
            });
        }
        
        [Test]
        [ExpectedException(typeof(AutoMapperMappingException))]
        public void Should_match_on_the_name_even_if_values_match()
        {
            var source = new FirstClass
            {
                EnumValue = FirstEnum.NamedEnum
            };
            _result = Mapper.Map<FirstClass, SecondClass>(source);
            _result.EnumValue.ShouldEqual(SecondEnum.DifferentNamedEnum);
        }
    }
}