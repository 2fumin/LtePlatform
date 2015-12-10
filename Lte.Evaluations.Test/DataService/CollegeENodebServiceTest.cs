using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.Test.TestService;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class CollegeENodebServiceTest
    {
        private readonly Mock<IInfrastructureRepository> _repository = new Mock<IInfrastructureRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<IAlarmRepository> _alarmRepository = new Mock<IAlarmRepository>();
        private CollegeENodebService _service;
        private CollegeENodebTestService _testService;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new CollegeENodebService(_repository.Object, _eNodebRepository.Object, _alarmRepository.Object);
            InfrastructureMapperService.MapENodeb();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockThreeENodebs();
            _repository.MockOperations();
            _alarmRepository.MockOperations();
            _testService = new CollegeENodebTestService(_repository, _eNodebRepository, _alarmRepository);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Test_QueryCollegeENodebs_SingleInfrastructure(int id)
        {
            _testService.MockOneENodebInfrastructure(id);
            _testService.MockOneAlarm("2015-1-1");
            var views = _service.QueryCollegeENodebs("College-" + id, DateTime.Parse("2014-12-30"),
                DateTime.Parse("2015-1-3"));
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

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Test_QueryCollegeENodebNames_SingleInfrastructure(int id)
        {
            _testService.MockOneENodebInfrastructure(id);
            var names = _service.QueryCollegeENodebNames("College-" + id);
            if (id > 0 && id <= 3)
            {
                Assert.AreEqual(names.Count(), 1);
                Assert.AreEqual(names.ElementAt(0), "ENodeb-" + id);
            }
            else
            {
                Assert.AreEqual(names.Count(), 0);
            }
        }

        [TestCase(1, "2015-12-30", "2015-1-2")]
        [TestCase(2, "2015-12-11", "2015-12-20")]
        [TestCase(3, "2015-12-30", "2015-1-2")]
        [TestCase(4, "2015-12-30", "2015-1-2")]
        [TestCase(5, "2015-12-11", "2015-12-20")]
        public void Test_QueryCollegeENodebs_SingleInfrastructure_DateConsidered(int id, 
            string beginDate, string endDate)
        {
            _testService.MockOneENodebInfrastructure(id);
            _testService.MockOneAlarm("2015-1-1");
            var views = _service.QueryCollegeENodebs("College-" + id, DateTime.Parse(beginDate),
                DateTime.Parse(endDate));
            Assert.IsNotNull(views);

            if (id > 0 && id <= 3)
            {
                Assert.AreEqual(views.Count(), 1, "normal case");
                Assert.AreEqual(views.ElementAt(0).AlarmTimes,
                    DateTime.Parse("2015-1-1") >= DateTime.Parse(beginDate) &&
                    DateTime.Parse("2015-1-1") < DateTime.Parse(endDate)
                        ? 1
                        : 0);
            }
            else
            {
                Assert.AreEqual(views.Count(), 0, "abnormal case");
            }
        }
    }
}
