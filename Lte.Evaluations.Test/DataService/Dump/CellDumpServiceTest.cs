using System.Collections.Generic;
using AutoMapper.Should;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService.Dump
{
    [TestFixture]
    public class CellDumpServiceTest : CellDumpServiceTestBase
    {
        [SetUp]
        public void Setup()
        {
            CellRepository.MockSixCells();
            CellRepository.MockOperations();
        }

        [TestCase(1, 2)]
        [TestCase(3, 4)]
        [TestCase(5, 6)]
        public void Test_DumpNewCellExcels_SingleItem(int eNodebId, byte sectorId)
        {
            var cellExcels = new List<CellExcel>
            {
                new CellExcel
                {
                    ENodebId = eNodebId,
                    SectorId = sectorId
                }
            };
            Service.DumpNewCellExcels(cellExcels);
            CellRepository.Object.Count().ShouldEqual(7);
            var lastObject = CellRepository.Object.GetAllList()[6];
            lastObject.ENodebId.ShouldEqual(eNodebId);
            lastObject.SectorId.ShouldEqual(sectorId);
        }

        [TestCase(1, 2)]
        [TestCase(3, 4)]
        [TestCase(5, 6)]
        public void Test_DumpSingleExcel(int eNodebId, byte sectorId)
        {
            var cellExcel = new CellExcel
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                ShareCdmaInfo = "1"
            };
            Assert.IsTrue(Service.DumpSingleCellExcel(cellExcel));
            CellRepository.Object.Count().ShouldEqual(7);
            var lastObject = CellRepository.Object.GetAllList()[6];
            lastObject.ENodebId.ShouldEqual(eNodebId);
            lastObject.SectorId.ShouldEqual(sectorId);
        }

        [TestCase(1, 2, "1_2_2", 2)]
        [TestCase(2, 2, "1_2_2", 2)]
        [TestCase(4, 2, "1_2_2", 2)]
        [TestCase(1, 2, "1_4_2", -1)]
        public void Test_UpdateENodebBtsIds_SingleItem(int eNodebId, byte sectorId, string shareInfo, int btsId)
        {
            var cellExcels = new List<CellExcel>
            {
                new CellExcel
                {
                    ENodebId = eNodebId,
                    SectorId = sectorId,
                    ShareCdmaInfo = shareInfo
                }
            };
            Service.UpdateENodebBtsIds(cellExcels);
            if (btsId > 0)
            {
                BtsRepository.Object.GetByBtsId(btsId).ENodebId.ShouldEqual(eNodebId);
            }
        }
    }
}
