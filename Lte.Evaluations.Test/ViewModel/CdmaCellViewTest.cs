using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.ViewModel
{
    [TestFixture]
    public class CdmaCellViewTest
    {
        private readonly Mock<IBtsRepository> _repository = new Mock<IBtsRepository>();
            
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            InfrastructureMapperService.MapCdmaCell();
            _repository.MockOperation();
            _repository.MockThreeBtss();
        }

        [TestCase(1, 2, 3, true, "室外", 12.3, 1.1, 1.3, "0x1122")]
        [TestCase(1, 4, 3, true, "室外", 12.6, 1.7, 1.3, "0x1192")]
        [TestCase(2, 4, 7, false, "室内", 14.6, 3.7, 2.3, "0x1192")]
        [TestCase(9, 4, 7, false, "室内", 14.6, 3.7, 2.3, "0x1192")]
        public void Test_Construct(int btsId, byte sectorId, int cellId, bool outdoor, string indoor, 
            double height, double eTilt, double mTilt, string lac)
        {
            var cell = new CdmaCell
            {
                BtsId = btsId,
                SectorId = sectorId,
                CellId = cellId,
                IsOutdoor = outdoor,
                Height = height,
                ETilt = eTilt,
                MTilt = mTilt,
                Lac = lac
            };
            var view = CdmaCellView.ConstructView(cell, _repository.Object);
            if (btsId > 0 && btsId <= 3)
                Assert.AreEqual(view.BtsName, "Bts-" + btsId);
            else
            {
                Assert.IsNull(view.BtsName);
            }
            Assert.AreEqual(view.BtsId, btsId);
            Assert.AreEqual(view.SectorId, sectorId);
            Assert.AreEqual(view.CellId, cellId);
            Assert.AreEqual(view.Indoor, indoor);
            Assert.AreEqual(view.Height, height);
            Assert.AreEqual(view.DownTilt, eTilt + mTilt);
            Assert.AreEqual(view.Lac, lac);
        }
    }
}
