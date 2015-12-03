using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.DataService.Dump;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class ENodebDumpServiceTest
    {
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>();
        private ENodebDumpService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new ENodebDumpService(_eNodebRepository.Object, _townRepository.Object);
            _eNodebRepository.MockRepositorySaveItems<ENodeb, IENodebRepository>();
            _eNodebRepository.MockOperations();
        }
    }
}
