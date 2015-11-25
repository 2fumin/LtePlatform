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
    public static class MockInfrastructureService
    {
        public static void MockOperations(this Mock<IInfrastructureRepository> repository)
        {
            repository.Setup(x => x.GetENodebIds(It.IsAny<string>()))
                .Returns<string>(collegeName => repository.Object.GetAll().Where(x =>
                    x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.ENodeb)
                    .Select(x => x.InfrastructureId).ToList());

            repository.Setup(x => x.GetBtsIds(It.IsAny<string>()))
                .Returns<string>(collegeName => repository.Object.GetAll().Where(x =>
                    x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.CdmaBts)
                    .Select(x => x.InfrastructureId).ToList());

            repository.Setup(x => x.GetCellIds(It.IsAny<string>()))
                .Returns<string>(collegeName => repository.Object.GetAll().Where(x =>
                    x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.Cell
                    ).Select(x => x.InfrastructureId).ToList());

            repository.Setup(x => x.GetCdmaCellIds(It.IsAny<string>()))
                .Returns<string>(collegeName => repository.Object.GetAll().Where(x =>
                    x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.CdmaCell
                    ).Select(x => x.InfrastructureId).ToList());
        }

        public static void MockThreeCollegeENodebs(this Mock<IInfrastructureRepository> repository)
        {
            repository.MockInfrastructures(new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-1",
                    HotspotType = HotspotType.College,
                    Id = 1,
                    InfrastructureType = InfrastructureType.ENodeb,
                    InfrastructureId = 1
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-2",
                    HotspotType = HotspotType.College,
                    Id = 2,
                    InfrastructureType = InfrastructureType.ENodeb,
                    InfrastructureId = 2
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-3",
                    HotspotType = HotspotType.College,
                    Id = 3,
                    InfrastructureType = InfrastructureType.ENodeb,
                    InfrastructureId = 3
                }
            });
        }
        
        public static void MockSixCollegeCells(this Mock<IInfrastructureRepository> repository)
        {
            repository.MockInfrastructures(new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-1",
                    HotspotType = HotspotType.College,
                    Id = 1,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 1
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-2",
                    HotspotType = HotspotType.College,
                    Id = 2,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 2
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-3",
                    HotspotType = HotspotType.College,
                    Id = 3,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 3
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-4",
                    HotspotType = HotspotType.College,
                    Id = 4,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 4
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-5",
                    HotspotType = HotspotType.College,
                    Id = 5,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 5
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-6",
                    HotspotType = HotspotType.College,
                    Id = 6,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 6
                }
            });
        }

        public static void MockSixCollegeCdmaCells(this Mock<IInfrastructureRepository> repository)
        {
            repository.MockInfrastructures(new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-1",
                    HotspotType = HotspotType.College,
                    Id = 1,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 1
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-2",
                    HotspotType = HotspotType.College,
                    Id = 2,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 2
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-3",
                    HotspotType = HotspotType.College,
                    Id = 3,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 3
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-4",
                    HotspotType = HotspotType.College,
                    Id = 4,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 4
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-5",
                    HotspotType = HotspotType.College,
                    Id = 5,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 5
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-6",
                    HotspotType = HotspotType.College,
                    Id = 6,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 6
                }
            });
        }
    }
}
