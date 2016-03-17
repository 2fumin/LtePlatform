using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.Test.TestService;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService.College
{
    [TestFixture]
    public class CollegeBtssServiceTest
    {
        private readonly Mock<IInfrastructureRepository> _repository = new Mock<IInfrastructureRepository>();
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private CollegeBtssService _service;
        private CollegeBtsTestService _testService;

        [TestFixtureSetUp]
        public void TestFixtureService()
        {
            AutoMapperHelper.CreateMap(typeof(CdmaBtsView));
            _btsRepository.MockOperation();
            _btsRepository.MockThreeBtss();
            _repository.MockOperations();
            _service = new CollegeBtssService(_repository.Object, _btsRepository.Object);
            _testService = new CollegeBtsTestService(_repository, _btsRepository);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Test_QueryCollegeBtss_SingleInfrastructure(int id)
        {
            _testService.MockOneBtsInfrastructure(id);
            var views = _service.QueryCollegeBtss("College-" + id);
            Assert.IsNotNull(views);

            if (id > 0 && id <= 3)
            {
                Assert.AreEqual(views.Count(), 1);
            }
            else
            {
                Assert.AreEqual(views.Count(), 0);
            }
        }
    }
}
