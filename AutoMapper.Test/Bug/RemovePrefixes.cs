using System;
using AutoMapper.Mappers;
using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class RemovePrefixes : AutoMapperSpecBase
    {
        ConfigurationStore config;

        class Source
        {
            public int GetNumber { get; set; }
        }
        class Destination
        {
            public int Number { get; set; }
        }

        protected override void Establish_context()
        {
            config = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            config.ClearPrefixes();
            config.CreateMap<Source, Destination>();
        }

        [Test]
        public void Should_not_map_with_default_postfix()
        {
            new Action(config.AssertConfigurationIsValid).ShouldThrow<AutoMapperConfigurationException>();
        }
    }
}