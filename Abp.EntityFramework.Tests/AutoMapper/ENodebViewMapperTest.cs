using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Entities;
using NUnit.Framework;
using Shouldly;

namespace Abp.EntityFramework.Tests.AutoMapper
{
    [TestFixture]
    public class ENodebViewMapperTest
    {
        [SetUp]
        public void Setup()
        {
            AutoMapperHelper.CreateMap(typeof(ENodebView));
        }

        [Test]
        public void Map_Null_Tests()
        {
            ENodeb eNodeb = null;
            var eNodebView = eNodeb.MapTo<ENodebView>();
            eNodebView.ShouldBeNull();
        }

        [Test]
        public void Map_ENodebId_Test()
        {
            var eNodeb = new ENodeb {ENodebId = 22};
            var eNodebView = eNodeb.MapTo<ENodebView>();
            eNodebView.ShouldNotBeNull();
            eNodebView.ENodebId.ShouldBe(22);
        }

        [Test]
        public void Map_Name_Test()
        {
            var eNodeb = new ENodeb {Name = "abcde"};
            var eNodebView = eNodeb.MapTo<ENodebView>();
            eNodebView.ShouldNotBeNull();
            eNodebView.Name.ShouldBe("abcde");
        }
    }
}
