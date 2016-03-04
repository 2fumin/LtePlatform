using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockRegionService
    {
        public static void MockOperation(this Mock<IRegionRepository> repository)
        {
            repository.Setup(x => x.GetAllList(It.IsAny<string>()))
                .Returns<string>(city => repository.Object.GetAll().Where(x => x.City == city).ToList());

            repository.Setup(x => x.GetAllListAsync(It.IsAny<string>()))
                .Returns<string>(city => Task.Run(() =>
                    repository.Object.GetAll().Where(x => x.City == city).ToList()));
        }
    }
}
