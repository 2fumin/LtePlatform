using System;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract.College;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService.College
{
    [TestFixture]
    public class CollegeKpiServiceTest
    {
        private readonly Mock<ICollegeKpiRepository> _repository = new Mock<ICollegeKpiRepository>();
        private readonly Mock<ICollegeRepository> _collegeRepository = new Mock<ICollegeRepository>();
        private CollegeKpiService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new CollegeKpiService(_repository.Object, _collegeRepository.Object);
            _collegeRepository.MockThreeColleges();
            _collegeRepository.MockOpertions();
            _repository.MockOperations();
            AutoMapperHelper.CreateMap(typeof(CollegeKpiView));
        }

        [TestCase(1, "2015-10-10", 4, 1.5)]
        [TestCase(2, "2015-11-10", 3, 34.50)]
        [TestCase(3, "2015-08-07", 11, 213.2)]
        public void Test_GetViews_OneKpiItem(int collegeId, string testDate, int hour, double kpi)
        {
            _repository.MockOneItem(collegeId, DateTime.Parse(testDate).AddHours(hour), kpi);
            var views = _service.GetViews(DateTime.Parse(testDate), hour).ToList();
            Assert.IsNotNull(views);
            Assert.AreEqual(views.Count, 1);
            views[0].AssertKpis(collegeId, kpi);
        }

        [TestCase(1, new[] { 1, 2 }, "2015-10-10", 4, new[] { 1.5, 11 })]
        [TestCase(2, new[] { 2, 3 }, "2015-10-10", 4, new[] { 15, 1.1 })]
        [TestCase(3, new[] { 1, 2, 3 }, "2015-7-10", 4, new[] { 15, 11, 1.8 })]
        [TestCase(4, new[] { 2, 3, 1 }, "2015-6-10", 9, new[] { 14, 1.1, 88 })]
        public void Test_GetViews_MultiItems_SingleTestDate(int testNo, int[] collegeIds, string testDate, int hour,
            double[] kpis)
        {
            _repository.MockItems(collegeIds, DateTime.Parse(testDate).AddHours(hour), kpis);
            var views = _service.GetViews(DateTime.Parse(testDate), hour).ToList();
            Assert.IsNotNull(views);
            Assert.AreEqual(views.Count, collegeIds.Length);
            for (var i = 0; i < collegeIds.Length; i++)
            {
                views[i].AssertKpis(collegeIds[i], kpis[i]);
            }
        }

        [TestCase(1, new[] { 1, 2 }, new[] { "2015-10-10", "2015-4-7" }, 4, new[] { 1.5, 11 })]
        [TestCase(2, new[] { 2, 3 }, new[] { "2015-10-10", "2015-4-7" }, 4, new[] { 15, 1.1 })]
        [TestCase(3, new[] { 1, 2, 3 }, new[] { "2015-10-10", "2015-4-7", "2015-9-9" }, 4, new[] { 1.5, 11, 18 })]
        [TestCase(4, new[] { 2, 3, 1 }, new[] { "2015-10-10", "2015-4-7", "2015-7-8" }, 9, new[] { 14, 11, 8.8 })]
        public void Test_GetResult_MultiItems_SingleTestDate(int testNo, int[] collegeIds, string[] testDates, int hour,
            double[] kpis)
        {
            _repository.MockItems(collegeIds, testDates.Select(x => DateTime.Parse(x).AddHours(hour)).ToArray(), kpis);

            for (var i = 0; i < collegeIds.Length; i++)
            {
                var views = _service.GetViews(DateTime.Parse(testDates[i]), hour).ToList();
                Assert.IsNotNull(views);
                views[0].AssertKpis(collegeIds[i], kpis[i]);
            }
        }

    }
}
