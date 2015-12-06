using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Should;
using Lte.Evaluations.DataService.Dump;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService.Dump
{
    [TestFixture]
    public class CellDumpServiceTest
    {
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();
        private CellDumpService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new CellDumpService(_btsRepository.Object, _cellRepository.Object);
            _btsRepository.MockOperation();
            _btsRepository.MockThreeBtss();
            _cellRepository.MockOperations();
            _cellRepository.MockRepositorySaveItems<Cell,ICellRepository>();
            CoreMapperService.MapCell();
            ParametersDumpMapperService.MapENodebBtsIdService();
        }

        [SetUp]
        public void Setup()
        {
            _cellRepository.MockSixCells();
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
            _service.DumpNewCellExcels(cellExcels);
            _cellRepository.Object.Count().ShouldEqual(7);
            var lastObject = _cellRepository.Object.GetAllList()[6];
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
            _service.UpdateENodebBtsIds(cellExcels);
            if (btsId > 0)
            {
                _btsRepository.Object.GetByBtsId(btsId).ENodebId.ShouldEqual(eNodebId);
            }
        }
    }
}
