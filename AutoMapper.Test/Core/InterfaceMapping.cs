using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Core
{
    namespace InterfaceMapping
    {
        [TestFixture]
        public class When_mapping_an_interface_to_an_abstract_type : AutoMapperSpecBase
        {
            private DtoObject _result;

            public class ModelObject
            {
                public IChildModelObject Child { get; set; }
            }

            public interface IChildModelObject
            {
                string ChildProperty { get; set; }
            }

            public class SubChildModelObject : IChildModelObject
            {
                public string ChildProperty { get; set; }
            }

            public class DtoObject
            {
                public DtoChildObject Child { get; set; }
            }

            public abstract class DtoChildObject
            {
                public virtual string ChildProperty { get; set; }
            }

            public class SubDtoChildObject : DtoChildObject
            {
            }

            protected override void Establish_context()
            {
                Mapper.Reset();

                var model = new ModelObject
                {
                    Child = new SubChildModelObject {ChildProperty = "child property value"}
                };

                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ModelObject, DtoObject>();

                    cfg.CreateMap<IChildModelObject, DtoChildObject>()
                          .Include<SubChildModelObject, SubDtoChildObject>();

                    cfg.CreateMap<SubChildModelObject, SubDtoChildObject>();
                });

                Mapper.AssertConfigurationIsValid();

                _result = Mapper.Map<ModelObject, DtoObject>(model);
            }

            [Test]
            public void Should_map_Child_to_SubDtoChildObject_type()
            {
                _result.Child.ShouldBeType(typeof (SubDtoChildObject));
            }

            [Test]
            public void Should_map_ChildProperty_to_child_property_value()
            {
                _result.Child.ChildProperty.ShouldBe("child property value");
            }
        }

#if !SILVERLIGHT && !NETFX_CORE

        [TestFixture]
        public class When_mapping_a_concrete_type_to_an_interface_type : AutoMapperSpecBase
        {
            private IDestination _result;

            public class Source
            {
                public int Value { get; set; }
            }

            public class Destination : IDestination
            {
                public int Value { get; set; }
            }

            public interface IDestination
            {
                int Value { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.CreateMap<Source, Destination>();
            }

            protected override void Because_of()
            {
                _result = Mapper.Map<Source, Destination>(new Source {Value = 5});
            }

            [Test]
            public void Should_create_an_implementation_of_the_interface()
            {
                _result.Value.ShouldBe(5);
            }

            [Test]
            public void Should_not_derive_from_INotifyPropertyChanged()
            {
                _result.ShouldNotBeInstanceOf<INotifyPropertyChanged>();    
            }
        }

        [TestFixture]
        public class When_mapping_a_concrete_type_to_an_interface_type_that_derives_from_INotifyPropertyChanged : AutoMapperSpecBase
        {
            private IDestination _result;

            private int _count;

            public class Source
            {
                public int Value { get; set; }
            }

            public interface IDestination : INotifyPropertyChanged
            {
                int Value { get; set; }
            }

            public class Destination : IDestination
            {
                public event PropertyChangedEventHandler PropertyChanged;

                private int _value;

                public int Value
                {
                    get { return _value; }
                    set
                    {
                        _value = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
                    }
                }
            }

            protected override void Establish_context()
            {
                Mapper.CreateMap<Source, Destination>();
            }

            protected override void Because_of()
            {
                _result = Mapper.Map<Source, Destination>(new Source {Value = 5});
            }

            [Test]
            public void Should_create_an_implementation_of_the_interface()
            {
                _result.Value.ShouldBe(5);
            }

            [Test]
            public void Should_derive_from_INotifyPropertyChanged()
            {
                var q = _result as INotifyPropertyChanged;
                q.ShouldNotBeNull();
            }

            [Test]
            public void Should_notify_property_changes()
            {
                var count = 0;
                _result.PropertyChanged += (o, e) => {
                    count++;
                    o.ShouldBeSameAs(_result); 
                    e.PropertyName.ShouldBe("Value");
                };

                _result.Value = 42;
                count.ShouldBe(1);
                _result.Value.ShouldBe(42);
            }

            [Test]
            public void Should_detach_event_handler()
            {
                _result.PropertyChanged += MyHandler;
                _count.ShouldBe(0);

                _result.Value = 56;
                _count.ShouldBe(1);

                _result.PropertyChanged -= MyHandler;
                _count.ShouldBe(1);

                _result.Value = 75;
                _count.ShouldBe(1);
            }

            private void MyHandler(object sender, PropertyChangedEventArgs e) {
                _count++;
            }
        }
#endif

        [TestFixture]
        public class When_mapping_a_derived_interface_to_an_derived_concrete_type : AutoMapperSpecBase
        {
            private Destination _result = null;

            public interface ISourceBase
            {
                int Id { get; }
            }

            public interface ISource : ISourceBase
            {
                int SecondId { get; }
            }

            public class Source : ISource
            {
                public int Id { get; set; }
                public int SecondId { get; set; }
            }

            public abstract class DestinationBase
            {
                public int Id { get; set; }
            }

            public class Destination : DestinationBase
            {
                public int SecondId { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.CreateMap<ISource, Destination>();
            }

            protected override void Because_of()
            {
                _result = Mapper.Map<ISource, Destination>(new Source {Id = 7, SecondId = 42});
            }

            [Test]
            public void Should_map_base_interface_property()
            {
                _result.Id.ShouldBe(7);
            }

            [Test]
            public void Should_map_derived_interface_property()
            {
                _result.SecondId.ShouldBe(42);
            }

            [Test]
            public void Should_pass_configuration_testing()
            {
                Mapper.AssertConfigurationIsValid();
            }
        }

        [TestFixture]
        public class When_mapping_a_derived_interface_to_an_derived_concrete_type_with_readonly_interface_members :
            AutoMapperSpecBase
        {
            private Destination _result = null;

            public interface ISourceBase
            {
                int Id { get; }
            }

            public interface ISource : ISourceBase
            {
                int SecondId { get; }
            }

            public class Source : ISource
            {
                public int Id { get; set; }
                public int SecondId { get; set; }
            }

            public interface IDestinationBase
            {
                int Id { get; }
            }

            public interface IDestination : IDestinationBase
            {
                int SecondId { get; }
            }

            public abstract class DestinationBase : IDestinationBase
            {
                public int Id { get; set; }
            }

            public class Destination : DestinationBase, IDestination
            {
                public int SecondId { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.CreateMap<ISource, Destination>();
            }

            protected override void Because_of()
            {
                _result = Mapper.Map<ISource, Destination>(new Source {Id = 7, SecondId = 42});
            }

            [Test]
            public void Should_map_base_interface_property()
            {
                _result.Id.ShouldBe(7);
            }

            [Test]
            public void Should_map_derived_interface_property()
            {
                _result.SecondId.ShouldBe(42);
            }

            [Test]
            public void Should_pass_configuration_testing()
            {
                Mapper.AssertConfigurationIsValid();
            }
        }

        [TestFixture]
        public class When_mapping_to_a_type_with_explicitly_implemented_interface_members : AutoMapperSpecBase
        {
            private Destination _destination;

            public class Source
            {
                public int Value { get; set; }
            }

            public interface IOtherDestination
            {
                int OtherValue { get; set; }
            }

            public class Destination : IOtherDestination
            {
                public int Value { get; set; }
                int IOtherDestination.OtherValue { get; set; }
            }

            protected override void Establish_context()
            {
                Mapper.CreateMap<Source, Destination>();
            }

            protected override void Because_of()
            {
                _destination = Mapper.Map<Source, Destination>(new Source {Value = 10});
            }

            [Test]
            public void Should_ignore_interface_members_for_mapping()
            {
                _destination.Value.ShouldBe(10);
            }

            [Test]
            public void Should_ignore_interface_members_for_validation()
            {
                Mapper.AssertConfigurationIsValid();
            }
        }

        [TestFixture]
        public class MappingToInterfacesWithPolymorphism : AutoMapperSpecBase
        {
            private BaseDto[] _baseDtos;

            public interface IBase { }
            public interface IDerived : IBase { }
            public class Base : IBase { }
            public class Derived : Base, IDerived { }
            public class BaseDto { }
            public class DerivedDto : BaseDto { }

            //and following mappings:
            protected override void Establish_context()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Base, BaseDto>().Include<Derived, DerivedDto>();
                    cfg.CreateMap<Derived, DerivedDto>();
                    cfg.CreateMap<IBase, BaseDto>().Include<IDerived, DerivedDto>();
                    cfg.CreateMap<IDerived, DerivedDto>();
                });
            }
            protected override void Because_of()
            {
                List<Base> list = new List<Base>() { new Derived() };
                _baseDtos = Mapper.Map<IEnumerable<Base>, BaseDto[]>(list);
            }

            [Test]
            public void Should_use_the_derived_type_map()
            {
                _baseDtos.First().ShouldBeType<DerivedDto>();
            }

        }

        [TestFixture, Explicit]
        public class MappingToInterfacesPolymorphic
        {
            [SetUp]
            public void SetUp()
            {
                Mapper.Reset();
            }

            public interface DomainInterface
            {
                Guid Id { get; set; }
                NestedType Nested { get; set; }
            }

            public sealed class NestedType
            {
                public string Name { get; set; }
                public decimal DecimalValue { get; set; }
            }

            public sealed class DomainImplA : DomainInterface
            {
                public Guid Id { get; set; }
                private NestedType nested;

                public NestedType Nested
                {
                    get { return nested ?? (nested = new NestedType()); }
                    set { nested = value; }
                }
            }

            public sealed class DomainImplB : DomainInterface
            {
                public Guid Id { get; set; }
                private NestedType nested;

                public NestedType Nested
                {
                    get { return nested ?? (nested = new NestedType()); }
                    set { nested = value; }
                }
            }

            public class Dto
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public decimal DecimalValue { get; set; }
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void CanMapToDomainInterface()
            {
                Mapper.CreateMap<DomainInterface, Dto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nested.Name))
                    .ForMember(dest => dest.DecimalValue, opt => opt.MapFrom(src => src.Nested.DecimalValue));
                Mapper.CreateMap<Dto, DomainInterface>()
                    .ForMember(dest => dest.Nested.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Nested.DecimalValue, opt => opt.MapFrom(src => src.DecimalValue));

                var domainInstance1 = new DomainImplA();
                var domainInstance2 = new DomainImplB();
                var domainInstance3 = new DomainImplA();

                var dtoCollection = new List<Dto>
                {
                    Mapper.Map<DomainInterface, Dto>(domainInstance1),
                    Mapper.Map<DomainInterface, Dto>(domainInstance2),
                    Mapper.Map<DomainInterface, Dto>(domainInstance3)
                };

                dtoCollection[0].Id = Guid.NewGuid();
                dtoCollection[0].DecimalValue = 1M;
                dtoCollection[0].Name = "Bob";
                dtoCollection[1].Id = Guid.NewGuid();
                dtoCollection[1].DecimalValue = 0.1M;
                dtoCollection[1].Name = "Frank";
                dtoCollection[2].Id = Guid.NewGuid();
                dtoCollection[2].DecimalValue = 2.1M;
                dtoCollection[2].Name = "Sam";

                Mapper.Map<Dto, DomainInterface>(dtoCollection[0], domainInstance1);
                Mapper.Map<Dto, DomainInterface>(dtoCollection[1], domainInstance2);
                Mapper.Map<Dto, DomainInterface>(dtoCollection[2], domainInstance3);

                dtoCollection[0].Id.ShouldBe(domainInstance1.Id);
                dtoCollection[1].Id.ShouldBe(domainInstance2.Id);
                dtoCollection[2].Id.ShouldBe(domainInstance3.Id);

                dtoCollection[0].DecimalValue.ShouldBe(domainInstance1.Nested.DecimalValue);
                dtoCollection[1].DecimalValue.ShouldBe(domainInstance2.Nested.DecimalValue);
                dtoCollection[2].DecimalValue.ShouldBe(domainInstance3.Nested.DecimalValue);

                dtoCollection[0].Name.ShouldBe(domainInstance1.Nested.Name);
                dtoCollection[1].Name.ShouldBe(domainInstance2.Nested.Name);
                dtoCollection[2].Name.ShouldBe(domainInstance3.Nested.Name);
            }
        }
    }
}