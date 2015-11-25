using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockBtsService
    {
        public static void MockThreeBtss(this Mock<IBtsRepository> repository)
        {
            repository.MockBtss(new List<CdmaBts>
            {
                new CdmaBts {Id = 1, BtsId = 1, Name = "Bts-1"},
                new CdmaBts {Id = 2, BtsId = 2, Name = "Bts-2"},
                new CdmaBts {Id = 3, BtsId = 3, Name = "Bts-3"}
            });
        }

        public static void MockOperation(this Mock<IBtsRepository> repository)
        {
            repository.Setup(x => x.GetByBtsId(It.IsAny<int>()))
                .Returns<int>(btsId => repository.Object.GetAll().FirstOrDefault(x => x.BtsId == btsId));

            repository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => repository.Object.GetAll().FirstOrDefault(x => x.Id == id));
        }
    }
}
