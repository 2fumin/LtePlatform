using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    // Bug #511
    // https://github.com/AutoMapper/AutoMapper/issues/511
    public class ReadOnlyCollectionMappingBug
    {
        class Source { public int X { get; set; } }
        class Target { public int X { get; set; } }

        [Test]
        public void Example()
        {
            Mapper.CreateMap<Source, Target>();

            var source = new List<Source> { new Source { X = 42 } };
            var target = Mapper.Map<ReadOnlyCollection<Target>>(source);

            target.Count.ShouldEqual(source.Count);
            target[0].X.ShouldEqual(source[0].X);
        }
    }
}
