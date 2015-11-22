using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    public class NullableEnumToNullableValueType
    {
        [TestFixture]
        public class CannotConvertEnumToNullableWhenPassedNull : AutoMapperSpecBase
        {
            public enum DummyTypes : int
            {
                Foo = 1,
                Bar = 2
            }

            public class DummySource
            {
                public DummyTypes? Dummy { get; set; }
            }

            public class DummyDestination
            {
                public int? Dummy { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.CreateMap<DummySource, DummyDestination>();
            }

            [Test]
            public void Should_map_null_enum_to_nullable_base_type()
            {
                var src = new DummySource() { Dummy = null };

                var destination = Mapper.Map<DummySource, DummyDestination>(src);

                destination.Dummy.ShouldBeNull();
            }
        } 
    }
}