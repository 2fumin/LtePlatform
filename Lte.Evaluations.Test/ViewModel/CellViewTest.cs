using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.ViewModel
{
    [TestFixture]
    public class CellViewTest
    {
        private readonly Mock<IENodebRepository> _repository = new Mock<IENodebRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            InfrastructureMapperService.MapCell();
            _repository.MockOperations();
            _repository.MockThreeENodebs();
        }

        [TestCase(1, 2, true, "室外", 12.3, 1.1, 1.3, 1122)]
        [TestCase(1, 4, true, "室外", 12.6, 1.7, 1.3, 11192)]
        [TestCase(2, 4, false, "室内", 14.6, 3.7, 2.3, 1354)]
        [TestCase(9, 4, false, "室内", 14.6, 3.7, 2.3, 2067)]
        public void Test_Construct(int eNodebId, byte sectorId, bool outdoor, string indoor,
            double height, double eTilt, double mTilt, int tac)
        {
            var cell = new Cell
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                IsOutdoor = outdoor,
                Height = height,
                ETilt = eTilt,
                MTilt = mTilt,
                Tac = tac
            };
            var view = CellView.ConstructView(cell, _repository.Object);
            if (eNodebId > 0 && eNodebId <= 3)
                Assert.AreEqual(view.ENodebName, "ENodeb-" + eNodebId);
            else
            {
                Assert.IsNull(view.ENodebName);
            }
            Assert.AreEqual(view.ENodebId, eNodebId);
            Assert.AreEqual(view.SectorId, sectorId);
            Assert.AreEqual(view.Indoor, indoor);
            Assert.AreEqual(view.Height, height);
            Assert.AreEqual(view.DownTilt, eTilt + mTilt);
            Assert.AreEqual(view.Tac, tac);
        }
    }
}
