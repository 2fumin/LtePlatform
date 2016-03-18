using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockTop3GConnectionService
    {
        public static void MockOperation(this Mock<ITopConnection3GRepository> repository)
        {
            repository.Setup(x => x.Import(It.IsAny<IEnumerable<TopConnection3GCellExcel>>()))
                .Returns<IEnumerable<TopConnection3GCellExcel>>(stats => stats.Count());
        }
    }
}
