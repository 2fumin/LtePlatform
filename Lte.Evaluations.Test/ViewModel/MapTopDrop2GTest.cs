using AutoMapper;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Evaluations.Test.ViewModel
{
    [TestFixture]
    public class MapTopDrop2GTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
        }

        [Test]
        public void Test()
        {
            KpiMapperService.MapTopKpi();
            var source = new TopCellContainer<TopDrop2GCell>
            {
                LteName = "aaa",
                CdmaName = "bbb",
                TopCell = new TopDrop2GCell
                {
                    CallAttempts = 1001,
                    Drops = 21
                }
            };
            var dest = Mapper.Map<TopCellContainer<TopDrop2GCell>, TopDrop2GCellViewContainer>(source);
            Assert.AreEqual(dest.LteName,"aaa");
            Assert.AreEqual(dest.CdmaName,"bbb");
            Assert.AreEqual(dest.TopDrop2GCellView.CallAttempts,1001);
            Assert.AreEqual(dest.TopDrop2GCellView.Drops,21);
        }
    }
}
