using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
    namespace DestinationCtorCalledTwice
    {
        [TestFixture]
        public class Bug : AutoMapperSpecBase
        {
            public class Source
            {
                public int Value { get; set; }
            }
            public class Destination
            {
                public Destination()
                {
                    CallCount++;
                }

                public int Value { get; set; }

                public static int CallCount { get; private set; }

                public static void Reset()
                {
                    CallCount = 0;
                }
            }

            public Bug()
            {
                Destination.Reset();
            }

            [Test]
            public void Should_call_ctor_once()
            {
                var source = new Source {Value = 5};
                var dest = new Destination {Value = 7};

                Mapper.Map(source, dest, opt => opt.CreateMissingTypeMaps = true);

                Destination.CallCount.ShouldBe(1);
            }
        }
    }
}