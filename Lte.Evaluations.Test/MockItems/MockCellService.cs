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
    public static class MockCellService
    {
        public static void MockOperations(this Mock<ICellRepository> repository)
        {
            repository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => repository.Object.GetAll().FirstOrDefault(x => x.Id == id));

            repository.Setup(x => x.GetBySectorId(It.IsAny<int>(), It.IsAny<byte>()))
                .Returns<int, byte>(
                    (eNodebId, sectorId) =>
                        repository.Object.GetAll().FirstOrDefault(x => x.ENodebId == eNodebId && x.SectorId == sectorId));
        }

        public static void MockSixCells(this Mock<ICellRepository> repository)
        {
            repository.MockCells(new List<Cell>
            {
                new Cell
                {
                    Id = 1,
                    ENodebId = 1,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2
                },
                new Cell
                {
                    Id = 2,
                    ENodebId = 2,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2
                },
                new Cell
                {
                    Id = 3,
                    ENodebId = 2,
                    SectorId = 2,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2
                },
                new Cell
                {
                    Id = 4,
                    ENodebId = 3,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2
                },
                new Cell
                {
                    Id = 5,
                    ENodebId = 3,
                    SectorId = 2,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2
                },
                new Cell
                {
                    Id = 6,
                    ENodebId = 3,
                    SectorId = 3,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2
                }
            });
        }
    }
}
