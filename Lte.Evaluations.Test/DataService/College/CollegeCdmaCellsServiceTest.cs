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
    public class CollegeCdmaCellsServiceTest
    {
        private readonly Mock<IInfrastructureRepository> _repository = new Mock<IInfrastructureRepository>();
        private readonly Mock<ICdmaCellRepository> _cellRepository = new Mock<ICdmaCellRepository>();
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private CollegeCdmaCellsService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new CollegeCdmaCellsService(_repository.Object, _cellRepository.Object, _btsRepository.Object);
            BaiduMapperService.MapCdmaCellView();
            InfrastructureMapperService.MapCdmaCell();
            _repository.MockOperations();
            _repository.MockSixCollegeCdmaCells();
            _cellRepository.MockOperations();
            _btsRepository.MockOperation();
            _btsRepository.MockThreeBtss();
        }

        [TestCase(1, true, "Bts-1-1", 112.3, 23.2, 30, "室外")]
        [TestCase(2, true, "Bts-2-1", 112.3, 23.2, 60, "室外")]
        [TestCase(3, true, "Bts-2-2", 112.3, 23.2, 90, "室外")]
        [TestCase(4, true, "Bts-3-1", 112.3, 23.2, 150, "室外")]
        [TestCase(5, true, "Bts-3-2", 112.3, 23.2, 210, "室外")]
        [TestCase(6, true, "Bts-3-3", 112.3, 23.2, 270, "室外")]
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
                var cellViews = views as CdmaCellView[] ?? views.ToArray();
                Assert.AreEqual(cellViews.Count(), 1);
                cellViews.ElementAt(0)
                    .AssertEqual(cellName, lontitute + GeoMath.BaiduLongtituteOffset,
                        lattitute + GeoMath.BaiduLattituteOffset, azimuth, indoor);
            }
        }

        [TestCase(1, true, "Bts-1-1", 112.3, 23.2, 30, "室外")]
        [TestCase(2, true, "Bts-2-1", 112.3, 23.2, 60, "室外")]
        [TestCase(3, true, "Bts-2-2", 112.3, 23.2, 90, "室外")]
        [TestCase(4, true, "Bts-3-1", 112.3, 23.2, 150, "室外")]
        [TestCase(5, true, "Bts-3-2", 112.3, 23.2, 210, "室外")]
        [TestCase(6, true, "Bts-3-3", 112.3, 23.2, 270, "室外")]
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
