using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class College3GTestServiceTest
    {
        private readonly Mock<ICollege3GTestRepository> _repository = new Mock<ICollege3GTestRepository>();
        private readonly Mock<ICollegeRepository> _collegeRepository = new Mock<ICollegeRepository>();
        private College3GTestService _service;

        [TestFixtureSetUp]
        public void TfSetup()
        {
            _service = new College3GTestService(_repository.Object, _collegeRepository.Object);
            _collegeRepository.MockThreeColleges();
            _collegeRepository.MockOpertions();
            _repository.MockOperations();
            CollegeMapperService.MapCollege3GTest();
        }
        
        [Test]
        public void Test_MockValues()
        {
            Assert.IsNotNull(_collegeRepository.Object.GetAll().FirstOrDefault(x => x.Id == 1));
            Assert.IsNotNull(_collegeRepository.Object.Get(1));
            Assert.IsNotNull(_collegeRepository.Object.Get(2));
            Assert.IsNotNull(_collegeRepository.Object.Get(3));
            Assert.IsNotNull(_collegeRepository.Object.GetByName("college-1"));
            Assert.IsNotNull(_collegeRepository.Object.GetByName("college-2"));
            Assert.IsNotNull(_collegeRepository.Object.GetByName("college-3"));
        }

        [TestCase(3, "2015-3-3")]
        [TestCase(4, "2015-4-7 12:33")]
        [TestCase(7, "2012-3-7 11:38")]
        public void Test_MockTestRepository(int id, string time)
        {
            _repository.MockOneItem(id, time);
            Assert.IsNotNull(_repository.Object.GetByCollegeIdAndTime(id, DateTime.Parse(time)));
        }
        
        [TestCase(1, "2015-10-10", 4, 15)]
        [TestCase(2, "2015-11-10", 3, 3450)]
        [TestCase(3, "2015-08-07", 11, 2132)]
        public void Test_GetViews_OneTestItem(int collegeId, string testDate, int hour, int users)
        {
            _repository.MockOneItem(collegeId, DateTime.Parse(testDate).AddHours(hour), users);

            var views = _service.GetViews(DateTime.Parse(testDate), hour).ToList();
            Assert.IsNotNull(views);
            Assert.AreEqual(views.Count,1);
            views[0].AssertUsers(collegeId, users);
        }

        [TestCase(1, "2015-10-10", 4, 15)]
        [TestCase(2, "2015-11-10", 3, 3450)]
        [TestCase(3, "2015-08-07", 11, 2132)]
        public void Test_GetResult_OneTestItem(int collegeId, string testDate, int hour, int users)
        {
            _repository.MockOneItem(collegeId, DateTime.Parse(testDate).AddHours(hour), users);

            var result = _service.GetResult(DateTime.Parse(testDate), hour, "college-" + collegeId);
            Assert.IsNotNull(result, "the result is null");
            result.AssertUsers(users);
        }

        [TestCase(1, new[] {1, 2}, "2015-10-10", 4, new[] {15, 11})]
        [TestCase(2, new[] { 2, 3 }, "2015-10-10", 4, new[] { 15, 11 })]
        [TestCase(3, new[] { 1, 2, 3 }, "2015-7-10", 4, new[] { 15, 11, 18 })]
        [TestCase(4, new[] { 2, 3, 1 }, "2015-6-10", 9, new[] { 14, 11, 88 })]
        public void Test_GetViews_MultiItems_SingleTestDate(int testNo, int[] collegeIds, string testDate, int hour,
            int[] users)
        {
            _repository.MockItems(collegeIds, DateTime.Parse(testDate).AddHours(hour), users);
            var views = _service.GetViews(DateTime.Parse(testDate), hour).ToList();
            Assert.IsNotNull(views);
            Assert.AreEqual(views.Count, collegeIds.Length);
            for (var i = 0; i < collegeIds.Length; i++)
            {
                views[i].AssertUsers(collegeIds[i], users[i]);
            }
        }

        [TestCase(1, new[] { 1, 2 }, new[] { "2015-10-10", "2015-4-7"}, 4, new[] { 15, 11 })]
        [TestCase(2, new[] { 2, 3 }, new[] { "2015-10-10", "2015-4-7" }, 4, new[] { 15, 11 })]
        [TestCase(3, new[] { 1, 2, 3 }, new[] { "2015-10-10", "2015-4-7", "2015-9-9" }, 4, new[] { 15, 11, 18 })]
        [TestCase(4, new[] { 2, 3, 1 }, new[] { "2015-10-10", "2015-4-7", "2015-7-8" }, 9, new[] { 14, 11, 88 })]
        public void Test_GetResult_MultiItems_SingleTestDate(int testNo, int[] collegeIds, string[] testDates, int hour,
            int[] users)
        {
            _repository.MockItems(collegeIds, testDates.Select(x => DateTime.Parse(x).AddHours(hour)).ToArray(), users);

            for (var i = 0; i < collegeIds.Length; i++)
            {
                var result = _service.GetResult(DateTime.Parse(testDates[i]), hour, "college-" + collegeIds[i]);
                Assert.IsNotNull(result, "the result is null");
                result.AssertUsers(users[i]);
            }
        }

        [TestCase(1, new[] { 1, 2 }, new[] { "2015-10-10", "2015-4-7" }, 4, new[] { 15, 11.0 }, "2015-4-1", "2015-10-11", 2)]
        [TestCase(2, new[] { 2, 3 }, new[] { "2015-10-10", "2015-4-7" }, 4, new[] { 15.0, 11 }, "2015-5-2", "2015-10-12", 1)]
        [TestCase(3, new[] { 1, 2, 3 }, new[] { "2015-10-10", "2015-4-7", "2015-9-9" }, 4, new[] { 15, 11.0, 18 }, "2015-3-1", "2015-10-17", 3)]
        [TestCase(4, new[] { 2, 3, 1 }, new[] { "2015-10-10", "2015-4-7", "2015-7-8" }, 9, new[] { 14, 11.2, 88 }, "2015-7-3", "2015-9-8", 1)]
        [TestCase(5, new[] { 1, 2, 3, 2 }, new[] { "2015-10-10", "2015-4-7", "2015-9-9", "2015-4-17" }, 
            4, new[] { 15, 11.0, 18, 13 }, "2015-3-1", "2015-10-17", 3)]
        public void Test_GetAverageRates(int testNo,
            int[] collegeIds, string[] testDates, int hour, double[] rates,
            string begin, string end, int resultNum)
        {
            _repository.MockRateItems(collegeIds, testDates.Select(x => DateTime.Parse(x).AddHours(hour)).ToArray(), rates);

            var result = _service.GetAverageRates(DateTime.Parse(begin), DateTime.Parse(end));
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, resultNum);
            for (var i = 0; i < result.Count; i++)
            {
                Console.WriteLine("Average Rate[{0}]: {1}", result.ElementAt(i).Key, result[result.ElementAt(i).Key]);
            }
        }
    }
}
