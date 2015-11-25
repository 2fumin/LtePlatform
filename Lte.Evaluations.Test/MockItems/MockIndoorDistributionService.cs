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
    public static class MockIndoorDistributionService
    {
        public static void MockOperations(this Mock<IIndoorDistributioinRepository> repository)
        {
            repository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => repository.Object.GetAll().FirstOrDefault(x => x.Id == id));
        }

        public static void MockThreeDistributions(this Mock<IIndoorDistributioinRepository> repository)
        {
            repository.MockDistributions(new List<IndoorDistribution>
            {
                new IndoorDistribution
                {
                    Id = 1,
                    Longtitute = 112.1,
                    Lattitute = 23.1,
                    Name = "Distribution-1",
                    Range = "Building",
                    SourceType = "RRU"
                },
                new IndoorDistribution
                {
                    Id = 2,
                    Longtitute = 112.1,
                    Lattitute = 23.1,
                    Name = "Distribution-2",
                    Range = "Building",
                    SourceType = "RRU"
                },
                new IndoorDistribution
                {
                    Id = 3,
                    Longtitute = 112.1,
                    Lattitute = 23.1,
                    Name = "Distribution-3",
                    Range = "Building",
                    SourceType = "RRU"
                }
            });
        }
    }
}
