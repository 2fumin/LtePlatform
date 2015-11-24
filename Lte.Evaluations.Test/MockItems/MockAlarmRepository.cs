using System;
using System.Linq;
using Lte.Parameters.Abstract;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockAlarmRepository
    {
        public static void MockOperations(this Mock<IAlarmRepository> repository)
        {
            repository.Setup(x => x.GetAllList(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) =>
                        repository.Object.GetAll().Where(x => x.HappenTime >= begin && x.HappenTime < end).ToList());
        }
    }
}
