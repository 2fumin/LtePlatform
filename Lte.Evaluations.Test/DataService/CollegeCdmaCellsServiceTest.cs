using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.DataService;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Abstract;
using Moq;

namespace Lte.Evaluations.Test.DataService
{
    public class CollegeCdmaCellsServiceTest
    {
        private readonly Mock<IInfrastructureRepository> _repository = new Mock<IInfrastructureRepository>();
        private readonly Mock<ICdmaCellRepository> _cellRepository = new Mock<ICdmaCellRepository>();
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private CollegeCdmaCellsService _service;

        public void TestFixtureSetup()
        {
            _service = new CollegeCdmaCellsService(_repository.Object, _cellRepository.Object, _btsRepository.Object);
            BaiduMapperService.MapCdmaCellView();
            InfrastructureMapperService.MapCdmaCell();
        }
    }
}
