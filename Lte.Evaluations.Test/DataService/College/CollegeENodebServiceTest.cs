using System;
using System.Collections.Generic;
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
using Shouldly;

namespace Lte.Evaluations.Test.DataService.College
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
            AutoMapperHelper.CreateMap(typeof(ENodebView));
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

        [TestCase("aaa", new[] {1}, 1)]
        [TestCase("aab", new[] { 1, 2 }, 2)]
        [TestCase("acb", new[] { 1, 2, 3 }, 3)]
        [TestCase("acbs", new[] { 1, 2, 3, 4 }, 3)]
        public void Test_QueryCollegeENodebs_FromENodebNames_OneCollege(string collegeName, int[] eNodebIds,
            int resultCounts)
        {
            _testService.MockManyENodebInfrastructure(new Dictionary<string, IEnumerable<int>>
            {
                {collegeName, eNodebIds }
            });
            var views = _service.QueryCollegeENodebs(new List<string> {collegeName});
            views.Count().ShouldBe(resultCounts);
        }

        [TestCase("aaa", "bbb", new[] { 1 }, new[] { 2 }, 2)]
        [TestCase("aaa", "bbb", new[] { 1 }, new[] { 1 }, 1)]
        [TestCase("aaa", "bbb", new[] { 1, 2 }, new[] { 2 }, 2)]
        [TestCase("aaa", "bbb", new[] { 1, 2 }, new[] { 3 }, 3)]
        [TestCase("aaa1", "bbb2", new[] { 1, 2 }, new[] { 2, 3 }, 3)]
        [TestCase("aaa13", "bbb2", new[] { 1, 2, 4 }, new[] { 2, 3 }, 3)]
        public void Test_QueryCollegeENodebs_FromENodebNames_TwoColleges(string collegeName1, string collegeName2,
            int[] eNodebIds1, int[] eNodebIds2, int resultCounts)
        {
            _testService.MockManyENodebInfrastructure(new Dictionary<string, IEnumerable<int>>
            {
                {collegeName1, eNodebIds1 },
                {collegeName2, eNodebIds2 }
            });
            var views = _service.QueryCollegeENodebs(new List<string>
            {
                collegeName1,
                collegeName2
            });
            views.Count().ShouldBe(resultCounts);
        }

        [TestCase("aaa", "bbb", "cccc", new[] { 1 }, new[] { 2 }, new[] { 3 }, 3)]
        [TestCase("aaa", "bbb", "cccc", new[] { 1 }, new[] { 2 }, new[] { 2 }, 2)]
        public void Test_QueryCollegeENodebs_FromENodebNames_ThreeColleges(string collegeName1, string collegeName2,
            string collegeName3, int[] eNodebIds1, int[] eNodebIds2, int[] eNodebIds3, int resultCounts)
        {
            _testService.MockManyENodebInfrastructure(new Dictionary<string, IEnumerable<int>>
            {
                {collegeName1, eNodebIds1 },
                {collegeName2, eNodebIds2 },
                {collegeName3, eNodebIds3 }
            });
            var views = _service.QueryCollegeENodebs(new List<string>
            {
                collegeName1,
                collegeName2,
                collegeName3
            });
            views.Count().ShouldBe(resultCounts);
        }
    }
}
