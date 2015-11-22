﻿using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class NullableEnums : AutoMapperSpecBase
    {
        public class Src { public EnumType? A { get; set; } }
        public class Dst { public EnumType? A { get; set; } }

        public enum EnumType { One, Two }

        protected override void Establish_context()
        {
            Mapper.CreateMap<Src, Dst>();
        }

        [Test]
        public void TestNullableEnum()
        {
            var d = Mapper.Map(new Src { A = null }, new Dst { A = EnumType.One });

            d.A.ShouldBeNull();
        } 
    }
}