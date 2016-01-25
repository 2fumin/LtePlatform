using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract.College;
using Lte.Parameters.Entities.College;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService.College
{
    public static class College3GTestQueries
    {
        public static void AssertUsers(this College3GTestView view, int collegeId, int users)
        {
            Assert.AreEqual(view.CollegeName, "college-" + collegeId);
            Assert.AreEqual(view.AccessUsers, users);
        }

        public static void AssertUsers(this College3GTestResults result, int users)
        {
            Assert.AreEqual(result.AccessUsers, users);
        }

        public static void MockOneItem(this Mock<ICollege3GTestRepository> repository, int id, string time)
        {
            repository.MockQueryItems(new List<College3GTestResults>
            {
                new College3GTestResults
                {
                    CollegeId = id,
                    TestTime = DateTime.Parse(time)
                }
            }.AsQueryable());
        }

        public static void MockOneItem(this Mock<ICollege3GTestRepository> repository, int id, DateTime time, int users)
        {
            repository.MockQueryItems(new List<College3GTestResults>
            {
                new College3GTestResults
                {
                    CollegeId = id,
                    TestTime = time,
                    AccessUsers = users
                }
            }.AsQueryable());
        }

        public static void MockItems(this Mock<ICollege3GTestRepository> repository, int[] collegeIds, DateTime time,
            int[] users)
        {
            var resultList = collegeIds.Select((t, i) => new College3GTestResults
            {
                CollegeId = t,
                TestTime = time,
                AccessUsers = users[i]
            }).ToList();
            repository.MockQueryItems(resultList.AsQueryable());
        }

        public static void MockItems(this Mock<ICollege3GTestRepository> repository, int[] collegeIds, DateTime[] times,
            int[] users)
        {
            var resultList = collegeIds.Select((t, i) => new College3GTestResults
            {
                CollegeId = t,
                TestTime = times[i],
                AccessUsers = users[i]
            }).ToList();
            repository.MockQueryItems(resultList.AsQueryable());
        }

        public static void MockRateItems(this Mock<ICollege3GTestRepository> repository, int[] collegeIds, DateTime[] times,
            double[] rates)
        {
            var resultList = collegeIds.Select((t, i) => new College3GTestResults
            {
                CollegeId = t,
                TestTime = times[i],
                DownloadRate = rates[i]
            }).ToList();
            repository.MockQueryItems(resultList.AsQueryable());
        }
    }
}
