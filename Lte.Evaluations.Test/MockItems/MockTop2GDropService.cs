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
        }
    }
}
