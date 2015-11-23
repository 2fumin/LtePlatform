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
    public static class MockInfrastructureService
    {
        public static void MockOperations(this Mock<IInfrastructureRepository> repository)
        {
            repository.Setup(x => x.GetIds(It.IsAny<string>()))
                .Returns<string>(collegeName => repository.Object.GetAll().Where(x =>
                    x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.ENodeb)
                    .Select(x => x.InfrastructureId).ToList());

            repository.Setup(x => x.GetCellIds(It.IsAny<string>()))
                .Returns<string>(collegeName => repository.Object.GetAll().Where(x =>
                    x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.Cell
                    ).Select(x => x.InfrastructureId).ToList());
        }
    }
}
