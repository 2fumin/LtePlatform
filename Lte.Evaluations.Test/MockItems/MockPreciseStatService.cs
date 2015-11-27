using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockPreciseStatService
    {
        public static void MockOperations(this Mock<IPreciseCoverage4GRepository> repository)
        {
            repository.Setup(
                x => x.GetAllList(It.IsAny<int>(), It.IsAny<byte>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<int, int, DateTime, DateTime>(
                    (cellId, sectorId, begin, end) =>
                        repository.Object.GetAll()
                            .Where(
                                x =>
                                    x.CellId == cellId && x.SectorId == sectorId && x.StatTime >= begin &&
                                    x.StatTime < end)
                            .ToList());
        }
    }
}
