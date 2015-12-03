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
    public static class MockTownService
    {
        public static void MockOpertion(this Mock<ITownRepository> repository)
        {
            repository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => repository.Object.GetAll().FirstOrDefault(x => x.Id == id));
            repository.Setup(x => x.GetAll(It.IsAny<string>()))
                .Returns<string>(city => repository.Object.GetAll().Where(x => x.CityName == city).ToList());
        }

        public static void MockSixTowns(this Mock<ITownRepository> repository)
        {
            var ids = new [] {1, 2, 3, 4, 5, 6};
            repository.MockTowns(ids.Select(x=>new Town
            {
                Id = x,
                CityName = "city-" + x,
                DistrictName = "district-" + x,
                TownName = "town-" + x
            }).ToList());
        }
    }
}
