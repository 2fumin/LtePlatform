using System.Linq;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.Test.TestService;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Abstract;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService.College
{
    [TestFixture] 
    public class CollegeCellsServiceTest
    {
        private readonly Mock<IInfrastructureRepository> _repository = new Mock<IInfrastructureRepository>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private CollegeCellsService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new CollegeCellsService(_repository.Object, _cellRepository.Object, _eNodebRepository.Object);
            BaiduMapperService.MapCellView();
            InfrastructureMapperService.MapCell();
            _repository.MockOperations();
            _repository.MockSixCollegeCells();
            _cellRepository.MockOperations();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockThreeENodebs();
        }

        [TestCase(1, true, "ENodeb-1-1", 112.3, 23.2, 30, "室外")]
        [TestCase(2, true, "ENodeb-2-1", 112.3, 23.2, 60, "室外")]
        [TestCase(3, true, "ENodeb-2-2", 112.3, 23.2, 90, "室外")]
        [TestCase(4, true, "ENodeb-3-1", 112.3, 23.2, 150, "室外")]
        [TestCase(5, true, "ENodeb-3-2", 112.3, 23.2, 210, "室外")]
        [TestCase(6, true, "ENodeb-3-3", 112.3, 23.2, 270, "室外")]
        [TestCase(7, false, "ENodeb-3-3", 112.3, 23.2, 270, "室外")]
        [TestCase(27, false, "ENodeb-3-3", 112.3, 23.2, 270, "室外")]
        public void Test_GetViews_SingleInfrastructure(int id, bool matched, string cellName, double lontitute,
            double lattitute, double azimuth, string indoor)
        {
            _cellRepository.MockSixCells(lontitute, lattitute);
            var views = _service.GetViews("College-" + id);
            if (matched)
            {
                Assert.IsNotNull(views);
                var cellViews = views as CellView[] ?? views.ToArray();
                Assert.AreEqual(cellViews.Count(), 1);
                cellViews.ElementAt(0)
                    .AssertEqual(cellName, lontitute + GeoMath.BaiduLongtituteOffset,
                        lattitute + GeoMath.BaiduLattituteOffset, azimuth, indoor);
            }
        }

        [TestCase(1, true, "ENodeb-1-1", 112.3, 23.2, 30, "室外")]
        [TestCase(2, true, "ENodeb-2-1", 112.3, 23.2, 60, "室外")]
        [TestCase(3, true, "ENodeb-2-2", 112.3, 23.2, 90, "室外")]
        [TestCase(4, true, "ENodeb-3-1", 112.3, 23.2, 150, "室外")]
        [TestCase(5, true, "ENodeb-3-2", 112.3, 23.2, 210, "室外")]
        [TestCase(6, true, "ENodeb-3-3", 112.3, 23.2, 270, "室外")]
        [TestCase(7, false, "ENodeb-3-3", 112.3, 23.2, 270, "室外")]
        [TestCase(27, false, "ENodeb-3-3", 112.3, 23.2, 270, "室外")]
        public void Test_QuerySectors_SingleInfrastructure(int id, bool matched, string cellName, double lontitute,
            double lattitute, double azimuth, string indoor)
        {
            _cellRepository.MockSixCells(lontitute, lattitute);
            var views = _service.QuerySectors("College-" + id);
            if (matched)
            {
                Assert.IsNotNull(views);
                var cellViews = views as SectorView[] ?? views.ToArray();
                Assert.AreEqual(cellViews.Count(), 1);
                cellViews.ElementAt(0)
                    .AssertEqual(cellName, lontitute + GeoMath.BaiduLongtituteOffset,
                        lattitute + GeoMath.BaiduLattituteOffset, azimuth, indoor);
            }
        }
    }
}
