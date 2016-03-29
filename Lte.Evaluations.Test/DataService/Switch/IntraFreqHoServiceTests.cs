using Abp.EntityFramework;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Concrete;
using Lte.Parameters.Concrete.Basic;
using Lte.Parameters.Concrete.Switch;
using Lte.Parameters.Entities.Basic;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.DataService.Switch
{
    [TestFixture]
    public class IntraFreqHoServiceTests
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository = new UeEUtranMeasurementRepository();
        private readonly ICellMeasGroupZteRepository _zteGroupRepository = new CellMeasGroupZteRepository();

        private readonly IEUtranCellMeasurementZteRepository _zteCellGroupRepository =
            new EUtranCellMeasurementZteRepository();

        private readonly IIntraFreqHoGroupRepository _huaweiCellHoRepository = new IntraFreqHoGroupRepository();
        private readonly IIntraRatHoCommRepository _huaweiENodebHoRepository = new IntraRatHoCommRepository();
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository = new CellHuaweiMongoRepository();

        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            InfrastructureMapperService.MapHoParametersService();
            _eNodebRepository.Setup(x => x.GetByENodebId(It.IsAny<int>())).Returns(new ENodeb
            {
                Factory = "中兴"
            });
            _eNodebRepository.Setup(x => x.GetByENodebId(It.Is<int>(id => id == 500814))).Returns(new ENodeb
            {
                Factory = "华为"
            });
        }

        [TestCase(500814)]
        public void Test_HuaweiInterFreqENodebQuery(int eNodebId)
        {
            var query = new HuaweiENodebQuery(_huaweiENodebHoRepository, eNodebId);
            Assert.IsNotNull(query);
            var result = query.Query();
            Assert.IsNotNull(result);
        }

        [TestCase(500814)]
        [TestCase(501766)]
        public void Test_ENodebQuery(int eNodebId)
        {
            var service = new IntraFreqHoService(_zteMeasurementRepository, _zteGroupRepository, _zteCellGroupRepository,
                _huaweiCellHoRepository, _huaweiENodebHoRepository, _huaweiCellRepository, _eNodebRepository.Object);
            var result = service.QueryENodebHo(eNodebId);
            Assert.IsNotNull(result);
        }
    }
}
