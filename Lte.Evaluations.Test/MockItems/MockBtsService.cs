using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockBtsService
    {
        public static void MockThreeBtss(this Mock<IBtsRepository> repository)
        {
            repository.MockBtss(new List<CdmaBts>
            {
                new CdmaBts {Id = 1, BtsId = 1, Name = "Bts-1", Address = "Address-1", TownId = 1 },
                new CdmaBts {Id = 2, BtsId = 2, Name = "Bts-2", Address = "Address-2", TownId = 2 },
                new CdmaBts {Id = 3, BtsId = 3, Name = "Bts-3", Address = "Address-3", TownId = 3 }
            });
        }

        public static void MockThreeBtss(this Mock<IBtsRepository> repository, int[] townIds)
        {
            repository.MockBtss(new List<CdmaBts>
            {
                new CdmaBts {Id = 1, BtsId = 1, Name = "Bts-1", TownId = townIds[0] },
                new CdmaBts {Id = 2, BtsId = 2, Name = "Bts-2", TownId = townIds[1] },
                new CdmaBts {Id = 3, BtsId = 3, Name = "Bts-3", TownId = townIds[2] }
            });
        }

        public static void MockSixBtssWithENodebId(this Mock<IBtsRepository> repository)
        {
            repository.MockBtss(new List<CdmaBts>
            {
                new CdmaBts {Id = 1, BtsId = 1, Name = "Bts-1", ENodebId = 1},
                new CdmaBts {Id = 2, BtsId = 2, Name = "Bts-2", ENodebId = 2},
                new CdmaBts {Id = 3, BtsId = 3, Name = "Bts-3", ENodebId = 3},
                new CdmaBts {Id = 4, BtsId = 4, Name = "Bts-4", ENodebId = 4},
                new CdmaBts {Id = 5, BtsId = 5, Name = "Bts-5", ENodebId = 5},
                new CdmaBts {Id = 6, BtsId = 6, Name = "Bts-6", ENodebId = 6}
            });
        }

        public static void MockOperation(this Mock<IBtsRepository> repository)
        {
            repository.Setup(x => x.GetByBtsId(It.IsAny<int>()))
                .Returns<int>(btsId => repository.Object.GetAll().FirstOrDefault(x => x.BtsId == btsId));

            repository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => repository.Object.GetAll().FirstOrDefault(x => x.Id == id));
        }
    }
}
