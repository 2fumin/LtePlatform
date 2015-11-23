using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.DataService;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class CollegePreciseServiceTest
    {
        private readonly Mock<IInfrastructureRepository> _repository = new Mock<IInfrastructureRepository>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<IPreciseCoverage4GRepository> _kpiRepository = new Mock<IPreciseCoverage4GRepository>();

        private CollegePreciseService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            KpiMapperService.MapCellPrecise();
            _service = new CollegePreciseService(_repository.Object, _cellRepository.Object, _eNodebRepository.Object,
                _kpiRepository.Object);
            _repository.MockOperations();
            _cellRepository.MockOperations();
            _cellRepository.MockSixCells();
        }
    }
}
