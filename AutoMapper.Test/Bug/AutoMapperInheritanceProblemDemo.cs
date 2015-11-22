using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class SettersInBaseClasses
    {
        public SettersInBaseClasses()
        {
            SetUp();
        }

        public static void SetUp(){
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source, GrandGrandChild>();
                cfg.CreateMap<Source, GrandChild>();
                cfg.CreateMap<Source, Child>();
                cfg.CreateMap<Source, GrandGrandChildPrivate>();
                cfg.CreateMap<Source, GrandChildPrivate>();
                cfg.CreateMap<Source, ChildPrivate>();
            });
        }

        [Test]
        public void PublicSetterInParentWorks()
        {
            var source = new Source {ParentProperty = "ParentProperty", ChildProperty = 1};
            var target = Mapper.Map<Source, Child>(source);
            target.ParentProperty.ShouldEqual(source.ParentProperty);
            target.ChildProperty.ShouldEqual(source.ChildProperty);
        }

        
        [Test]
        public void PublicSetterInGrandparentWorks()
        {
            var source = new Source {ParentProperty = "ParentProperty", ChildProperty = 1};
            var target = Mapper.Map<Source, GrandChild>(source);
            target.ParentProperty.ShouldEqual(source.ParentProperty);
            target.ChildProperty.ShouldEqual(source.ChildProperty);
        }

        [Test]
        public void PublicSetterInGrandGrandparentWorks()
        {
            var source = new Source {ParentProperty = "ParentProperty", ChildProperty = 1};
            var target = Mapper.Map<Source, GrandGrandChild>(source);
            target.ParentProperty.ShouldEqual(source.ParentProperty);
            target.ChildProperty.ShouldEqual(source.ChildProperty);
        }

        [Test]
        public void HasValidConfiguration()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void PrivateSetterInParentWorks()
        {
            var source = new Source {ParentProperty = "ParentProperty", ChildProperty = 1};
            var target = Mapper.Map<Source, ChildPrivate>(source);
            target.ParentProperty.ShouldEqual(source.ParentProperty);
            target.ChildProperty.ShouldEqual(source.ChildProperty);
        }

        [Test]
        public void PrivateSetterInGrandparentWorks()
        {
            var source = new Source {ParentProperty = "ParentProperty", ChildProperty = 1};
            var target = Mapper.Map<Source, GrandChildPrivate>(source);
            target.ParentProperty.ShouldEqual(source.ParentProperty);
            target.ChildProperty.ShouldEqual(source.ChildProperty);
        }

        [Test]
        public void PrivateSetterInGrandGrandparentWorks()
        {
            var source = new Source {ParentProperty = "ParentProperty", ChildProperty = 1};
            var target = Mapper.Map<Source, GrandGrandChildPrivate>(source);
            target.ParentProperty.ShouldEqual(source.ParentProperty);
            target.ChildProperty.ShouldEqual(source.ChildProperty);
        }
    }

    public class Source
    {
        public string ParentProperty { get; set; }
        public int ChildProperty{get; set;}
    }

    public class Parent {
        public string ParentProperty{get; set;}
    }

    public class Child : Parent {
        public int ChildProperty {get; set;}
    }

    public class GrandChild : Child {
    }

    public class GrandGrandChild : GrandChild {
    }

    public class ParentPrivate {
        public string ParentProperty{get; private set;}
    }

    public class ChildPrivate : ParentPrivate {
        public int ChildProperty {get;private set;}
    }

    public class GrandChildPrivate : ChildPrivate {
    }

    public class GrandGrandChildPrivate : GrandChildPrivate {
    }
}