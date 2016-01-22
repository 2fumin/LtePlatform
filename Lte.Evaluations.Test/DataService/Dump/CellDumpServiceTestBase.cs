using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public abstract class CellDumpServiceTestBase
    {
        protected readonly Mock<IBtsRepository> BtsRepository = new Mock<IBtsRepository>();
        protected readonly Mock<ICellRepository> CellRepository = new Mock<ICellRepository>();
        protected CellDumpService Service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Service = new CellDumpService(BtsRepository.Object, CellRepository.Object);
            BtsRepository.MockOperation();
            BtsRepository.MockThreeBtss();
            CellRepository.MockRepositorySaveItems<Cell, ICellRepository>();
            CoreMapperService.MapCell();
            ParametersDumpMapperService.MapENodebBtsIdService();
        }

    }
}
