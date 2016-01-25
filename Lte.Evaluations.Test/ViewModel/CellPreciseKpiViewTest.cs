using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.ViewModel
{
    [TestFixture]
    public class CellPreciseKpiViewTest
    {
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockThreeENodebs();
            KpiMapperService.MapCellPrecise();
        }

        [TestCase(1, 2, 3, 1.1, 2.2, 12.3, true, 11.2, 22.34, 17.2)]
        [TestCase(2, 2, 4, 1.1, 2.2, 12.3, true, 11.2, 22.34, 19.2)]
        [TestCase(1, 2, 3, 1.1, 2.2, 12.3, false, 12.2, 22.34, 17.2)]
        [TestCase(3, 2, 3, 1.1, 2.2, 12.3, false, 11.2, 22.34, 17.2)]
        [TestCase(4, 2, 4, 1.1, 2.2, 12.3, true, 11.7, 22.34, 17.2)]
        [TestCase(5, 2, 3, 1.1, 2.2, 12.3, false, 11.2, 22.34, 17.2)]
        public void Test_Construction(int eNodebId, byte sectorId, int frequency, double rsPower, double height,
            double azimuth, bool isOutdoor, double eTilt, double mTilt, double antennaGain)
        {
            var cell = new Cell
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                Frequency = frequency,
                RsPower = rsPower,
                Height = height,
                Azimuth = azimuth,
                IsOutdoor = isOutdoor,
                ETilt = eTilt,
                MTilt = mTilt,
                AntennaGain = antennaGain
            };
            var view = CellPreciseKpiView.ConstructView(cell, _eNodebRepository.Object);
            Assert.AreEqual(view.ENodebId, eNodebId);
            Assert.AreEqual(view.SectorId, sectorId);
            if (eNodebId > 0 && eNodebId <= 3)
                Assert.AreEqual(view.ENodebName, "ENodeb-" + eNodebId);
            else
            {
                Assert.IsNull(view.ENodebName);
            }
            Assert.AreEqual(view.Frequency, frequency);
            Assert.AreEqual(view.RsPower, rsPower);
            Assert.AreEqual(view.Height, height);
            Assert.AreEqual(view.Azimuth, azimuth);
            Assert.AreEqual(view.Indoor, isOutdoor ? "室外" : "室内");
            Assert.AreEqual(view.DownTilt, eTilt + mTilt);
            Assert.AreEqual(view.AntennaGain, antennaGain);
        }
    }
}
