using System.Collections.Generic;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Lte.Evaluations.DataService.Dump
{
    [TestFixture]
    public class CdmaCellDumpServiceTest
    {
        private readonly Mock<ICdmaCellRepository> _cellRepository = new Mock<ICdmaCellRepository>();
        private CdmaCellDumpService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service=new CdmaCellDumpService(_cellRepository.Object);
            _cellRepository.MockOperations();
            _cellRepository.MockRepositorySaveItems<CdmaCell, ICdmaCellRepository>();
            CoreMapperService.MapCell();
        }

        [SetUp]
        public void Setup()
        {
            _cellRepository.MockSixCells();
        }

        [TestCase(1, 2)]
        [TestCase(3, 4)]
        [TestCase(5, 6)]
        public void Test_DumpNewCellExcels_SingleItem(int btsId, byte sectorId)
        {
            var cellExcels = new List<CdmaCellExcel>
            {
                new CdmaCellExcel
                {
                    BtsId = btsId,
                    SectorId = sectorId
                }
            };
            _service.DumpNewCellExcels(cellExcels);
            _cellRepository.Object.Count().ShouldBe(7);
            var lastObject = _cellRepository.Object.GetAllList()[6];
            lastObject.BtsId.ShouldBe(btsId);
            lastObject.SectorId.ShouldBe(sectorId);
        }
    }
}
