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
    public static class MockENodebService
    {
        public static void MockOperations(this Mock<IENodebRepository> repository)
        {
            repository.Setup(x => x.GetByENodebId(It.IsAny<int>()))
                .Returns<int>(eNodebId => repository.Object.GetAll().FirstOrDefault(x => x.ENodebId == eNodebId));

            repository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => repository.Object.GetAll().FirstOrDefault(x => x.Id == id));

            repository.Setup(x => x.GetByName(It.IsAny<string>()))
                .Returns<string>(name => repository.Object.GetAll().FirstOrDefault(x => x.Name == name));

            repository.Setup(x => x.GetAllInUseList())
                .Returns(repository.Object.GetAll().Where(x => x.IsInUse).ToList());
        }

        public static void MockThreeENodebs(this Mock<IENodebRepository> repository)
        {
            repository.MockENodebs(new List<ENodeb>
            {
                new ENodeb {Id = 1, ENodebId = 1, Name = "ENodeb-1", TownId = 1, Address = "Address-1", PlanNum = "FSL-1"},
                new ENodeb {Id = 2, ENodebId = 2, Name = "ENodeb-2", TownId = 2, Address = "Address-2", PlanNum = "FSL-2"},
                new ENodeb {Id = 3, ENodebId = 3, Name = "ENodeb-3", TownId = 3, Address = "Address-3", PlanNum = "FSL-3"}
            });
        }
    }
}
