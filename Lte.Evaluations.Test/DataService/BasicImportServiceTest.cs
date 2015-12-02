﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Should;
using Lte.Evaluations.DataService;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class BasicImportServiceTest
    {
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private readonly Mock<ICdmaCellRepository> _cdmaCellRepository = new Mock<ICdmaCellRepository>();

        private BasicImportService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new BasicImportService(_eNodebRepository.Object, _cellRepository.Object, _btsRepository.Object,
                _cdmaCellRepository.Object);
            _eNodebRepository.MockOperations();
            _btsRepository.MockOperation();
            _cellRepository.MockOperations();
            _cdmaCellRepository.MockOperations();
            _eNodebRepository.MockThreeENodebs();
            _btsRepository.MockThreeBtss();
            _cellRepository.MockSixCells();
            _cdmaCellRepository.MockSixCells();
        }

        [TestCase(new[] { 1, 2, 3, 4 }, new[] { 4 })]
        [TestCase(new[] { 1, 2  }, new int[] { })]
        [TestCase(new[] { 1, 2, 4 }, new[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 })]
        public void Test_GetNewENodebExcels(int[] inputENodebIds, int[] outputENodebIds)
        {
            BasicImportService.ENodebExcels = inputENodebIds.Select(x => new ENodebExcel
            {
                ENodebId = x,
                Name = "ENodeb-" + x
            }).ToList();
            var results = _service.GetNewENodebExcels();
            results.Select(x => x.ENodebId).ToArray().ShouldEqual(outputENodebIds);
        }

        [TestCase(new[] { 1, 2, 3, 4 }, new[] { 4 })]
        [TestCase(new[] { 1, 2 }, new int[] { })]
        [TestCase(new[] { 1, 2, 4 }, new[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 })]
        public void Test_GetNewBtsExcels(int[] inputBtsIds, int[] outputBtsIds)
        {
            BasicImportService.BtsExcels = inputBtsIds.Select(x => new BtsExcel
            {
                BtsId = x,
                Name = "Bts-" + x
            }).ToList();
            var results = _service.GetNewBtsExcels();
            results.Select(x => x.BtsId).ToArray().ShouldEqual(outputBtsIds);
        }

        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 3 }, new[] { 4 }, new byte[] { 3 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 3 }, new byte[] { 1, 2, 3, 4 }, new[] { 3 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 2 }, new byte[] { 1, 2, 3, 4 }, new[] { 2 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 1 }, new[] { 4 }, new byte[] { 1 })]
        [TestCase(new[] { 1, 2 }, new byte[] { 1, 2 }, new int[] { }, new byte[] { })]
        [TestCase(new[] { 1, 2, 4 }, new byte[] { 1, 2, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new byte[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 }, new byte[] { 4, 5 })]
        [TestCase(new[] { 1, 1, 3, 1, 5 }, new byte[] { 1, 1, 3, 4, 5 }, new[] { 1, 5 }, new byte[] { 4, 5 })]
        public void Test_GetNewCellExcels(int[] inputENodebIds, byte[] inputSectorIds, int[] outputENodebIds,
            byte[] outputSectorIds)
        {
            BasicImportService.CellExcels = inputENodebIds.Select((t, i) => new CellExcel
            {
                ENodebId = t,
                SectorId = inputSectorIds[i]
            }).ToList();
            var results = _service.GetNewCellExcels();
            results.Select(x => x.ENodebId).ToArray().ShouldEqual(outputENodebIds);
            results.Select(x => x.SectorId).ToArray().ShouldEqual(outputSectorIds);
        }

        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 3 }, new[] { 4 }, new byte[] { 3 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 3 }, new byte[] { 1, 2, 3, 4 }, new[] { 3 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 2 }, new byte[] { 1, 2, 3, 4 }, new[] { 2 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 1 }, new[] { 4 }, new byte[] { 1 })]
        [TestCase(new[] { 1, 2 }, new byte[] { 1, 2 }, new int[] { }, new byte[] { })]
        [TestCase(new[] { 1, 2, 4 }, new byte[] { 1, 2, 4 }, new[] { 4 }, new byte[] { 4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new byte[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 }, new byte[] { 4, 5 })]
        [TestCase(new[] { 1, 1, 3, 1, 5 }, new byte[] { 1, 1, 3, 4, 5 }, new[] { 1, 5 }, new byte[] { 4, 5 })]
        public void Test_GetNewCdmaCellExcels(int[] inputBtsIds, byte[] inputSectorIds, int[] outputBtsIds,
            byte[] outputSectorIds)
        {
            BasicImportService.CdmaCellExcels = inputBtsIds.Select((t, i) => new CdmaCellExcel
            {
                BtsId = t,
                SectorId = inputSectorIds[i]
            }).ToList();
            var results = _service.GetNewCdmaCellExcels();
            results.Select(x => x.BtsId).ToArray().ShouldEqual(outputBtsIds);
            results.Select(x => x.SectorId).ToArray().ShouldEqual(outputSectorIds);
        }
    }
}