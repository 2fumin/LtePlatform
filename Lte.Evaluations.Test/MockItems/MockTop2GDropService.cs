using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockTop2GDropService
    {
        public static void MockOperation(this Mock<ITopDrop2GCellRepository> repository)
        {
            repository.Setup(x => x.Import(It.IsAny<IEnumerable<TopDrop2GCellExcel>>()))
                .Returns<IEnumerable<TopDrop2GCellExcel>>(stats => stats.Count());

            repository.Setup(x => x.GetAllList(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<string, DateTime, DateTime>((city, begin, end) =>
                repository.Object.GetAll().Where(x => x.City == city && x.StatTime >= begin && x.StatTime < end).ToList());
        }
    }
}
