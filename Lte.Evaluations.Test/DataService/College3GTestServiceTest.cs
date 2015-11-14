using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        
        [Test]
        public void Test_MockValues()
        {
            Assert.IsNotNull(_collegeRepository.Object.GetAll().FirstOrDefault(x => x.Id == 1));
            Assert.IsNotNull(_collegeRepository.Object.Get(1));
            Assert.IsNotNull(_collegeRepository.Object.Get(2));
            Assert.IsNotNull(_collegeRepository.Object.Get(3));
        }
        
        [TestCase(1, "2015-10-10", 4, 15)]
        public void Test_GetViews_OneTestItem(int collegId, string testDate, int hour, int users)
        {
            _repository.MockQueryItems(new List<College3GTestResults>
            {
                new College3GTestResults
                {
                    CollegeId = collegId,
                    TestTime = DateTime.Parse(testDate).AddHours(hour),
                    AccessUsers = users
                }
            }.AsQueryable());

            var views = _service.GetViews(DateTime.Parse(testDate), hour).ToList();
            Assert.IsNotNull(views);
            Assert.AreEqual(views.Count,1);
            Assert.AreEqual(views[0].CollegeName,"college-"+collegId);
            Assert.AreEqual(views[0].AccessUsers,users);
        }
    }
}
