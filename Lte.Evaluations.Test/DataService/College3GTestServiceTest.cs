using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.DataService;
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
            _collegeRepository.MockAuditedItems(new List<CollegeInfo>
            {
                new CollegeInfo
                {
                    Id = 1,
                    Name = "college-1"
                },
                new CollegeInfo
                {
                    Id = 2,
                    Name = "college-2"
                },
                new CollegeInfo
                {
                    Id = 3,
                    Name = "college-3"
                }
            }.AsQueryable());
            _collegeRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<int>(
                id => _collegeRepository.Object.GetAll().FirstOrDefault(
                    x => x.Id == id));
            _collegeRepository.Setup(x => x.GetByName(It.IsAny<string>())).Returns<string>(
                name => _collegeRepository.Object.GetAll().FirstOrDefault(
                    x => x.Name == name));
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
        
        [TestCase(1, "2015-10-10", 4, 15)]
        [TestCase(2, "2015-11-10", 3, 3450)]
        [TestCase(3, "2015-08-07", 11, 2132)]
        public void Test_GetViews_OneTestItem(int collegeId, string testDate, int hour, int users)
        {
            _repository.MockQueryItems(new List<College3GTestResults>
            {
                new College3GTestResults
                {
                    CollegeId = collegeId,
                    TestTime = DateTime.Parse(testDate).AddHours(hour),
                    AccessUsers = users
                }
            }.AsQueryable());

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
            _repository.MockQueryItems(new List<College3GTestResults>
            {
                new College3GTestResults
                {
                    CollegeId = collegeId,
                    TestTime = DateTime.Parse(testDate).AddHours(hour),
                    AccessUsers = users
                }
            }.AsQueryable());

            var result = _service.GetResult(DateTime.Parse(testDate), hour, "colleg-" + collegeId);
            Assert.IsNotNull(result);
            result.AssertUsers(users);
        }

        [TestCase(1, new[] {1, 2}, "2015-10-10", 4, new[] {15, 11})]
        [TestCase(2, new[] { 2, 3 }, "2015-10-10", 4, new[] { 15, 11 })]
        [TestCase(3, new[] { 1, 2, 3 }, "2015-7-10", 4, new[] { 15, 11, 18 })]
        [TestCase(4, new[] { 2, 3, 1 }, "2015-6-10", 9, new[] { 14, 11, 88 })]
        public void Test_GetViews_MultiItems_SingleTestDate(int testNo, int[] collegeIds, string testDate, int hour,
            int[] users)
        {
            var resultList = collegeIds.Select((t, i) => new College3GTestResults
            {
                CollegeId = t,
                TestTime = DateTime.Parse(testDate).AddHours(hour),
                AccessUsers = users[i]
            }).ToList();
            _repository.MockQueryItems(resultList.AsQueryable());
            var views = _service.GetViews(DateTime.Parse(testDate), hour).ToList();
            Assert.IsNotNull(views);
            Assert.AreEqual(views.Count, collegeIds.Length);
            for (var i = 0; i < collegeIds.Length; i++)
            {
                views[i].AssertUsers(collegeIds[i], users[i]);
            }
        }
    }
}
