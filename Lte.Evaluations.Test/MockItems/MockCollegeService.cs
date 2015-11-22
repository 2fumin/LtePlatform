using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockCollegeService
    {
        public static void MockOpertions(this Mock<ICollegeRepository> repository)
        {
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns<int>(
                id => repository.Object.GetAll().FirstOrDefault(
                    x => x.Id == id));
            repository.Setup(x => x.GetByName(It.IsAny<string>())).Returns<string>(
                name => repository.Object.GetAll().FirstOrDefault(
                    x => x.Name == name));
        }

        public static void MockOperations(this Mock<ICollege3GTestRepository> repository)
        {
            repository.Setup(x => x.GetByCollegeIdAndTime(It.IsAny<int>(), It.IsAny<DateTime>())
                ).Returns<int, DateTime>((id, time) => repository.Object.GetAll().FirstOrDefault(
                    x => x.CollegeId == id && x.TestTime == time));

            repository.Setup(x => x.GetByTimeSpan(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) => repository.Object.GetAll().Where(x => x.TestTime > begin && x.TestTime <= end));
        }

        public static void MockOperations(this Mock<ICollegeKpiRepository> repository)
        {
            repository.Setup(x => x.GetList(It.IsAny<DateTime>()))
                .Returns<DateTime>(time => repository.Object.GetAll().Where(x => x.TestTime == time).ToList());
        }

        public static void MockThreeColleges(this Mock<ICollegeRepository> repository)
        {
            repository.MockAuditedItems(new List<CollegeInfo>
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
        }
    }
}
