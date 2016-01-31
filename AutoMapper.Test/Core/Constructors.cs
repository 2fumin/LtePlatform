using System;
using System.Linq.Expressions;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Core
{
    namespace Constructors
    {
        [TestFixture]
        public class When_mapping_to_an_object_with_a_constructor_with_a_matching_argument : AutoMapperSpecBase
        {
            private Dest _dest;

            public class Source
            {
                public int Foo { get; set; }
                public int Bar { get; set; }
            }

            public class Dest
            {
                public int Foo { get; }

                public int Bar { get; set; }

                public Dest(int foo)
                {
                    Foo = foo;
                }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Source, Dest>());
            }

            protected override void Because_of()
            {
                Expression<Func<object, object>> ctor = (input) => new Dest((int)input);

                object o = ctor.Compile()(5);

                _dest = Mapper.Map<Source, Dest>(new Source { Foo = 5, Bar = 10 });
            }

            [Test]
            public void Should_map_the_constructor_argument()
            {
                _dest.Foo.ShouldBe(5);
            }

            [Test]
            public void Should_map_the_existing_properties()
            {
                _dest.Bar.ShouldBe(10);
            }
        }

        [TestFixture]
        public class When_mapping_to_an_object_with_a_private_constructor : AutoMapperSpecBase
        {
            private Dest _dest;

            public class Source
            {
                public int Foo { get; set; }
            }

            public class Dest
            {
                public int Foo { get; }

                private Dest(int foo)
                {
                    Foo = foo;
                }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Source, Dest>());
            }

            protected override void Because_of()
            {
                _dest = Mapper.Map<Source, Dest>(new Source { Foo = 5 });
            }

            [Test]
            public void Should_map_the_constructor_argument()
            {
                _dest.Foo.ShouldBe(5);
            }
        }

        [TestFixture]
        public class When_mapping_to_an_object_using_service_location : AutoMapperSpecBase
        {
            private Dest _dest;

            public class Source
            {
                public int Foo { get; set; }
            }

            public class Dest
            {
                private int _foo;
                private readonly int _addend;

                public int Foo
                {
                    get { return _foo + _addend; }
                    set { _foo = value; }
                }

                public Dest(int addend)
                {
                    _addend = addend;
                }

                public Dest()
                    : this(0)
                {
                }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.ConstructServicesUsing(t => new Dest(5));
                    cfg.CreateMap<Source, Dest>()
                        .ConstructUsingServiceLocator();
                });
            }

            protected override void Because_of()
            {
                _dest = Mapper.Map<Source, Dest>(new Source { Foo = 5 });
            }

            [Test]
            public void Should_map_with_the_custom_constructor()
            {
                _dest.Foo.ShouldBe(10);
            }
        }

        [TestFixture]
        public class When_mapping_to_an_object_using_contextual_service_location : AutoMapperSpecBase
        {
            private Dest _dest;

            public class Source
            {
                public int Foo { get; set; }
            }

            public class Dest
            {
                private int _foo;
                private readonly int _addend;

                public int Foo
                {
                    get { return _foo + _addend; }
                    set { _foo = value; }
                }

                public Dest(int addend)
                {
                    _addend = addend;
                }

                public Dest()
                    : this(0)
                {
                }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.ConstructServicesUsing(t => new Dest(5));
                    cfg.CreateMap<Source, Dest>()
                        .ConstructUsingServiceLocator();
                });
            }

            protected override void Because_of()
            {
                _dest = Mapper.Map<Source, Dest>(new Source { Foo = 5 }, opt => opt.ConstructServicesUsing(t => new Dest(6)));
            }

            [Test]
            public void Should_map_with_the_custom_constructor()
            {
                _dest.Foo.ShouldBe(11);
            }
        }

        [TestFixture]
        public class When_mapping_to_an_object_with_multiple_constructors_and_constructor_mapping_is_disabled : AutoMapperSpecBase
        {
            private Dest _dest;

            public class Source
            {
                public int Foo { get; set; }
                public int Bar { get; set; }
            }

            public class Dest
            {
                public int Foo { get; set; }

                public int Bar { get; set; }

                public Dest(int foo)
                {
                    throw new NotImplementedException();
                }

                public Dest() { }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg =>
                                      {
                                          cfg.DisableConstructorMapping();
                                          cfg.CreateMap<Source, Dest>();
                                      }
                    );
            }

            protected override void Because_of()
            {
                _dest = Mapper.Map<Source, Dest>(new Source { Foo = 5, Bar = 10 });
            }

            [Test]
            public void Should_map_the_existing_properties()
            {
                _dest.Foo.ShouldBe(5);
                _dest.Bar.ShouldBe(10);
            }
        }

        [TestFixture]
        public class UsingMappingEngineToResolveConstructorArguments : AutoMapperSpecBase
        {
            [Test]
            public void Should_resolve_constructor_arguments_using_mapping_engine()
            {
                Mapper.CreateMap<SourceBar, DestinationBar>();

                Mapper.CreateMap<SourceFoo, DestinationFoo>();

                var sourceBar = new SourceBar("fooBar");
                var sourceFoo = new SourceFoo(sourceBar);

                var destinationFoo = Mapper.Map<DestinationFoo>(sourceFoo);

                destinationFoo.Bar.FooBar.ShouldBe(sourceBar.FooBar);
            }


            public class DestinationFoo
            {
                public DestinationBar Bar { get; }

                public DestinationFoo(DestinationBar bar)
                {
                    Bar = bar;
                }
            }

            public class DestinationBar
            {
                public string FooBar { get; }

                public DestinationBar(string fooBar)
                {
                    FooBar = fooBar;
                }
            }

            public class SourceFoo
            {
                public SourceBar Bar { get; private set; }

                public SourceFoo(SourceBar bar)
                {
                    Bar = bar;
                }
            }

            public class SourceBar
            {
                public string FooBar { get; }

                public SourceBar(string fooBar)
                {
                    FooBar = fooBar;
                }
            }
        }

        [TestFixture]
        public class When_mapping_to_an_object_with_a_constructor_with_multiple_optional_arguments : AutoMapperSpecBase
        {
            [Test]
            public void Should_resolve_constructor_when_args_are_optional()
            {

                Mapper.CreateMap<SourceFoo, DestinationFoo>();

                var sourceBar = new SourceBar("fooBar");
                var sourceFoo = new SourceFoo(sourceBar);

                var destinationFoo = Mapper.Map<DestinationFoo>(sourceFoo);

                destinationFoo.Bar.ShouldBeNull();
                destinationFoo.Str.ShouldBe("hello");
            }


            public class DestinationFoo
            {
                public DestinationBar Bar { get; }

                public string Str { get; }

                public DestinationFoo(DestinationBar bar=null,string str="hello")
                {
                    Bar = bar;
                    Str = str;
                }
            }

            public class DestinationBar
            {
                public string FooBar { get; }

                public DestinationBar(string fooBar)
                {
                    FooBar = fooBar;
                }
            }

            public class SourceFoo
            {
                public SourceBar Bar { get; private set; }

                public SourceFoo(SourceBar bar)
                {
                    Bar = bar;
                }
            }

            public class SourceBar
            {
                public string FooBar { get; private set; }

                public SourceBar(string fooBar)
                {
                    FooBar = fooBar;
                }
            }
        }

        [TestFixture]
        public class When_mapping_to_an_object_with_a_constructor_with_single_optional_arguments : AutoMapperSpecBase
        {
            [Test]
            public void Should_resolve_constructor_when_arg_is_optional()
            {
                Mapper.CreateMap<SourceFoo, DestinationFoo>();

                var sourceBar = new SourceBar("fooBar");
                var sourceFoo = new SourceFoo(sourceBar);

                var destinationFoo = Mapper.Map<DestinationFoo>(sourceFoo);

                destinationFoo.Bar.ShouldBeNull();
            }


            public class DestinationFoo
            {
                public DestinationBar Bar { get; }

                public DestinationFoo(DestinationBar bar = null)
                {
                    Bar = bar;
                }
            }

            public class DestinationBar
            {
                public string FooBar { get; }

                public DestinationBar(string fooBar)
                {
                    FooBar = fooBar;
                }
            }

            public class SourceFoo
            {
                public SourceBar Bar { get; private set; }

                public SourceFoo(SourceBar bar)
                {
                    Bar = bar;
                }
            }

            public class SourceBar
            {
                public string FooBar { get; private set; }

                public SourceBar(string fooBar)
                {
                    FooBar = fooBar;
                }
            }
        }

        [TestFixture]
        public class When_mapping_to_an_object_with_a_constructor_with_string_optional_arguments : AutoMapperSpecBase
        {
            [Test]
            public void Should_resolve_constructor_when_string_args_are_optional()
            {
                Mapper.CreateMap<SourceFoo, DestinationFoo>();

                var sourceBar = new SourceBar("fooBar");
                var sourceFoo = new SourceFoo(sourceBar);

                var destinationFoo = Mapper.Map<DestinationFoo>(sourceFoo);

                destinationFoo.A.ShouldBe("a");
                destinationFoo.B.ShouldBe("b");
                destinationFoo.C.ShouldBe(3);
            }


            public class DestinationFoo
            {
                public string A { get; }

                public string B { get; }

                public int C { get; }

                public DestinationFoo(string a = "a",string b="b", int c = 3)
                {
                    A = a;
                    B = b;
                    C = c;
                }
            }

            public class DestinationBar
            {
                public string FooBar { get; }

                public DestinationBar(string fooBar)
                {
                    FooBar = fooBar;
                }
            }

            public class SourceFoo
            {
                public SourceBar Bar { get; private set; }

                public SourceFoo(SourceBar bar)
                {
                    Bar = bar;
                }
            }

            public class SourceBar
            {
                public string FooBar { get; private set; }

                public SourceBar(string fooBar)
                {
                    FooBar = fooBar;
                }
            }
        }

        [TestFixture]
        public class When_configuring_ctor_param_members : AutoMapperSpecBase
        {
            public class Source
            {
                public int Value { get; set; }
            }

            public class Dest
            {
                public Dest(int thing)
                {
                    Value1 = thing;
                }

                public int Value1 { get; }
            }

            protected override void Establish_context()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Source, Dest>().ForCtorParam("thing", opt => opt.MapFrom(src => src.Value));
                });
            }

            [Test]
            public void Should_redirect_value()
            {
                var dest = Mapper.Map<Source, Dest>(new Source {Value = 5});

                dest.Value1.ShouldBe(5);
            }
        }
    }
}