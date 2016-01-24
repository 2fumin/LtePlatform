using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Parameters.Test.Entities
{
    [TestFixture]
    public class IndoorDistributionTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            AutoMapperHelper.CreateMap(typeof(IndoorDistributionExcel));
        }

        [TestCase("adkdj", 113.44, 23.11)]
        [TestCase("adwedj", 113.414, 23.161)]
        [TestCase("a23dj", 113.454, 23.121)]
        [TestCase("aewrj", 113.844, 23.111)]
        [TestCase("aewwdj", 112.44, 23.119)]
        public void TestConstructor(string name, double longtitute, double lattitute)
        {
            var info = new IndoorDistributionExcel
            {
                Name = name,
                Range = "range",
                SourceName = "source",
                SourceType = "type",
                Longtitute = longtitute,
                Lattitute = lattitute
            };
            var item = IndoorDistribution.ConstructItem(info);
            Assert.AreEqual(item.Name, name);
            Assert.AreEqual(item.Range, "range");
            Assert.AreEqual(item.SourceName, "source");
            Assert.AreEqual(item.SourceType, "type");
            Assert.AreEqual(item.BaiduLongtitute, info.Longtitute + GeoMath.BaiduLongtituteOffset);
            Assert.AreEqual(item.BaiduLattitute, info.Lattitute + GeoMath.BaiduLattituteOffset);
        }
    }
}
