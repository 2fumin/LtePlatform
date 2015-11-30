using System;
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
    }
}
