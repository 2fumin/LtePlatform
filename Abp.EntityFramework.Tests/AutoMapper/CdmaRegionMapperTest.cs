using Abp.EntityFramework.AutoMapper;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using NUnit.Framework;
using Shouldly;

namespace Abp.EntityFramework.Tests.AutoMapper
{
    [TestFixture]
    public class CdmaRegionMapperTest
    {
        [SetUp]
        public void Setup()
        {
            AutoMapperHelper.CreateMap(typeof(CdmaRegionStat));
        }

        [Test]
        public void Map_Null_Tests()
        {
            CdmaRegionStatExcel info = null;
            var stat = info.MapTo<CdmaRegionStat>();
            stat.ShouldBeNull();
        }

        [Test]
        public void Map_Region_Tests()
        {
            var info = new CdmaRegionStatExcel
            {
                Region = "foshan1",
                Drop2GNum = 22
            };
            var stat = info.MapTo<CdmaRegionStat>();
            stat.Region.ShouldBe("foshan1");
            stat.Drop2GNum.ShouldBe(22);
        }
    }
}
