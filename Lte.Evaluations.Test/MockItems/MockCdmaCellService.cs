using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockCdmaCellService
    {
        public static void MockOperations(this Mock<ICdmaCellRepository> repository)
        {
            repository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => repository.Object.GetAll().FirstOrDefault(x => x.Id == id));

            repository.Setup(x => x.GetBySectorId(It.IsAny<int>(), It.IsAny<byte>()))
                .Returns<int, byte>(
                    (btsId, sectorId) =>
                        repository.Object.GetAll().FirstOrDefault(x => x.BtsId == btsId && x.SectorId == sectorId));
        }

        public static void MockSixCells(this Mock<ICdmaCellRepository> repository, double lon = 113.01, double lat = 23.01)
        {
            repository.MockCdmaCells(new List<CdmaCell>
            {
                new CdmaCell
                {
                    Id = 1,
                    BtsId = 1,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 30,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 2,
                    BtsId = 2,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 60,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 3,
                    BtsId = 2,
                    SectorId = 2,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 90,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 4,
                    BtsId = 3,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 150,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 5,
                    BtsId = 3,
                    SectorId = 2,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 210,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 6,
                    BtsId = 3,
                    SectorId = 3,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 270,
                    Longtitute = lon,
                    Lattitute = lat
                }
            });
        }
    }
}
