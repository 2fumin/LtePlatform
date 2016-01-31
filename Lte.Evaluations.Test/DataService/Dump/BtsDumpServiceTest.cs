using System.Collections.Generic;
using Abp.EntityFramework.AutoMapper;
using AutoMapper.Should;
using Lte.Evaluations.DataService.Dump;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.DataService.Queries;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.ExcelCsv;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Lte.Evaluations.Test.DataService.Dump
{
    [TestFixture]
    public class BtsDumpServiceTest
    {
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>();
        private BtsDumpService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new BtsDumpService(_btsRepository.Object, _townRepository.Object);
            _btsRepository.MockOperation();
            _btsRepository.MockRepositorySaveItems<CdmaBts, IBtsRepository>();
            _townRepository.MockOpertion();
            _townRepository.MockSixTowns();
            ParametersDumpMapperService.MapFromBtsContainerService();
            AutoMapperHelper.CreateMap(typeof(BtsExcel));
        }

        [SetUp]
        public void Setup()
        {
            _btsRepository.MockThreeBtss();
        }

        [TestCase("abc", "ieowue", 1, 2, 112.998939,22.34284)]
        [TestCase("arebc", "ieo--wue", 3, 4, 113.98748, 23.5786)]
        public void Test_SingleItem(string name, string address, int townId, int btsId, double longtitute, double lattitute)
        {
            var infos = new List<BtsExcel>
            {
                new BtsExcel
                {
                    Name = name,
                    Address = address,
                    BtsId = btsId,
                    Longtitute = longtitute,
                    Lattitute = lattitute,
                    DistrictName = "district-" + townId,
                    TownName = "town-" + townId
                }
            };
            _service.DumpBtsExcels(infos);
            _btsRepository.Object.Count().ShouldBe(4);
            _btsRepository.Object.GetAllList()[3].ShouldBe(name, address, townId, btsId, longtitute, lattitute);
        }
    }
}
