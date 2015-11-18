using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockTownService
    {
        public static void MockOpertion(this Mock<ITownRepository> repository)
        {
            repository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => repository.Object.GetAll().FirstOrDefault(x => x.Id == id));
            repository.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns<string>(city => repository.Object.GetAll().Where(x => x.CityName == city));
        }
    }
}
