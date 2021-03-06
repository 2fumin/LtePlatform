﻿using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class ENodebQueryServiceTest
    {
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private ENodebQueryService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new ENodebQueryService(_townRepository.Object, _eNodebRepository.Object);
            _townRepository.MockSixTowns();
            _townRepository.MockOpertion();
            AutoMapperHelper.CreateMap(typeof (ENodebView));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 0)]
        [TestCase(6, 0)]
        [TestCase(13, 0)]
        [TestCase(24, 0)]
        public void Test_GetByTownNames(int townId, int count)
        {
            _eNodebRepository.MockThreeENodebs();
            _eNodebRepository.MockOperations();
            var eNodebList = _service.GetByTownNames("city-" + townId, "district-" + townId, "town-" + townId) ?? new List<ENodebView>();
            Assert.AreEqual(eNodebList.Count(), count);
        }

        [TestCase("ENodeb", 3)]
        [TestCase("1", 1)]
        [TestCase("2", 1)]
        [TestCase("3", 1)]
        [TestCase("Address", 3)]
        [TestCase("FSL", 3)]
        public void Test_GetByGeneralName(string queryString, int count)
        {
            _eNodebRepository.MockThreeENodebs();
            _eNodebRepository.MockOperations();
            var eNodebList = _service.GetByGeneralName(queryString);
            Assert.AreEqual(eNodebList.Count(), count);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_GetByBtsId(int id)
        {
            _eNodebRepository.MockThreeENodebs();
            _eNodebRepository.MockOperations();
            var eNodeb = _service.GetByENodebId(id);
            Assert.AreEqual(eNodeb.Name, "ENodeb-" + id);
        }
    }
}
