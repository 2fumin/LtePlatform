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
    public class BtsViewMapperTest
    {
        [SetUp]
        public void Setup()
        {
            AutoMapperHelper.CreateMap(typeof(CdmaBtsView));
        }

        [Test]
        public void Map_Null_Tests()
        {
            CdmaBts bts = null;
            var btsView = bts.MapTo<CdmaBtsView>();
            btsView.ShouldBeNull();
        }

        [Test]
        public void Map_BtsId_Test()
        {
            var bts = new CdmaBts {BtsId = 23};
            var btsView = bts.MapTo<CdmaBtsView>();
            btsView.BtsId.ShouldBe(23);
        }

        [Test]
        public void Map_Address_Test()
        {
            var bts = new CdmaBts {Address = "oqwufjoiwofui"};
            var btsView = bts.MapTo<CdmaBtsView>();
            btsView.Address.ShouldBe("oqwufjoiwofui");
        }

        [Test]
        public void Map_TwoItems_Test()
        {
            var btsList = new List<CdmaBts>
            {
                new CdmaBts {Address = "123"},
                new CdmaBts {Address = "456"}
            };
            var btsViewList = btsList.MapTo<IEnumerable<CdmaBts>>();
            btsViewList.Count().ShouldBe(2);
            btsViewList.ElementAt(0).Address.ShouldBe("123");
            btsViewList.ElementAt(1).Address.ShouldBe("456");
        }
    }
}
