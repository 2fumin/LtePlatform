using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
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
    }
}
