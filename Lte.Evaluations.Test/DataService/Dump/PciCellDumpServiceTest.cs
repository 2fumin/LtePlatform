using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Should;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService.Dump
{
    [TestFixture]
    public class PciCellDumpServiceTest : CellDumpServiceTestBase
    {
        [TestCase(1, 1, 110, 0, 88)]
        [TestCase(2, 1, 109, 1, 232)]
        [TestCase(2, 2, 108, 2, 256)]
        [TestCase(3, 1, 23, 3, 87)]
        [TestCase(3, 2, 567, 4, 8787)]
        [TestCase(3, 3, 288, 5, 237)]
        public void Test_DumpNewCellExcels_SingleItem(int eNodebId, byte sectorId, short originPci, int index, short modifiedPci)
        {
            CellRepository.MockSixCells(pci: originPci);
            CellRepository.MockOperations();
            var cellExcels = new List<CellExcel>
            {
                new CellExcel
                {
                    ENodebId = eNodebId,
                    SectorId = sectorId,
                    Pci = modifiedPci
                }
            };
            Service.DumpNewCellExcels(cellExcels);
            CellRepository.Object.Count().ShouldEqual(6);
            var results = CellRepository.Object.GetAllList();
            for (int i = 0; i < 6; i++)
            {
                results[i].Pci.ShouldEqual(i == index ? modifiedPci : originPci);
            }
        }

        [TestCase(1, 1, 110, 0, 88)]
        [TestCase(2, 1, 109, 1, 232)]
        [TestCase(2, 2, 108, 2, 256)]
        [TestCase(3, 1, 23, 3, 87)]
        [TestCase(3, 2, 567, 4, 8787)]
        [TestCase(3, 3, 288, 5, 237)]
        public void Test_DumpNewCellExcels_SingleItem_ConsideredInUse(int eNodebId, byte sectorId, short originPci,
            int index, short modifiedPci)
        {
            CellRepository.MockSixCells(pci: originPci, isInUse: false);
            CellRepository.MockOperations();
            var cellExcels = new List<CellExcel>
            {
                new CellExcel
                {
                    ENodebId = eNodebId,
                    SectorId = sectorId,
                    Pci = modifiedPci
                }
            };
            Service.DumpNewCellExcels(cellExcels);
            CellRepository.Object.Count().ShouldEqual(6);
            var results = CellRepository.Object.GetAllList();
            for (int i = 0; i < 6; i++)
            {
                if (i == index)
                {
                    results[i].Pci.ShouldEqual(modifiedPci);
                    results[i].IsInUse.ShouldBeTrue();
                }
                else
                {
                    results[i].Pci.ShouldEqual(originPci);
                    results[i].IsInUse.ShouldBeFalse();
                }
            }
        }

        [TestCase(1, 1, 110, 0, 88)]
        [TestCase(2, 1, 109, 1, 232)]
        [TestCase(2, 2, 108, 2, 256)]
        [TestCase(3, 1, 23, 3, 87)]
        [TestCase(3, 2, 567, 4, 8787)]
        [TestCase(3, 3, 288, 5, 237)]
        public void Test_DumpSingleCell(int eNodebId, byte sectorId, short originPci, int index, short modifiedPci)
        {
            CellRepository.MockSixCells(pci: originPci);
            CellRepository.MockOperations();
            var cellExcel = new CellExcel
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                Pci = modifiedPci
            };
            Service.DumpSingleCellExcel(cellExcel);
            CellRepository.Object.Count().ShouldEqual(6);
            var results = CellRepository.Object.GetAllList();
            for (int i = 0; i < 6; i++)
            {
                results[i].Pci.ShouldEqual(i == index ? modifiedPci : originPci);
            }
        }
    }
}
