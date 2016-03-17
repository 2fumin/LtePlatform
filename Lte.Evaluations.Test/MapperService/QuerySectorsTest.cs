using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.MapperService
{
    [TestFixture]
    public class QuerySectorsTest
    {
        private readonly Mock<ICellRepository> _repository = new Mock<ICellRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private CellService _service;
             
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            BaiduMapperService.MapCellView();
            InfrastructureMapperService.MapCell();
            _eNodebRepository.MockThreeENodebs();
            _repository.MockRangeCells();
            _service = new CellService(_repository.Object, _eNodebRepository.Object);
        }

        [Test]
        public void TestQuerySectors()
        {
            var sectors = _service.QuerySectors(113, 114, 22, 23);
            Assert.AreEqual(sectors.Count(), 6);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 1)]
        [TestCase(3, 2)]
        [TestCase(3, 3)]
        public void TestQuerySectors_ExcludedOneCell(int eNodebId, byte sectorId)
        {
            var container = new SectorRangeContainer
            {
                West = 113,
                East = 114,
                South = 22,
                North = 23,
                ExcludedCells = new List<CellIdPair>
                {
                    new CellIdPair {CellId = eNodebId, SectorId = sectorId}
                }
            };
            var sectors = _service.QuerySectors(container);
            Assert.AreEqual(sectors.Count(), 5);
        }

        [Test]
        public void Test_GetExceptCells()
        {
            var container = new SectorRangeContainer
            {
                West = 113,
                East = 114,
                South = 22,
                North = 23,
                ExcludedCells = new List<CellIdPair>
                {
                    new CellIdPair {CellId = 1, SectorId = 2}
                }
            };
            var cells = new List<Cell>
            {
                new Cell {ENodebId = 1, SectorId = 2},
                new Cell {ENodebId = 3, SectorId = 4}
            };
            var excludeCells = from cell in cells
                join sector in container.ExcludedCells on new
                {
                    CellId = cell.ENodebId,
                    cell.SectorId
                } equals new
                {
                    sector.CellId,
                    sector.SectorId
                }
                select cell;
            Assert.AreEqual(excludeCells.Count(), 1);
        }
    }
}
