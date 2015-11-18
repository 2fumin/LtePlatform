using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.DataService;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;

namespace Lte.Evaluations.Test.TestService
{
    public class PreciseRegionStatTestService
    {
        private readonly Mock<ITownRepository> _townRepository;
        private readonly Mock<ITownPreciseCoverage4GStatRepository> _statRepository;

        public PreciseRegionStatTestService(Mock<ITownRepository> townRepository,
            Mock<ITownPreciseCoverage4GStatRepository> statRepository)
        {
            _townRepository = townRepository;
            _statRepository = statRepository;
        }

        public void ImportPreciseRecord(int townId, string statDate, int totalMrs, int firstNeighbors,
            int secondNeighbors, int thirdNeighbors)
        {
            _statRepository.MockPreciseRegionStats(new List<TownPreciseCoverage4GStat>
            {
                new TownPreciseCoverage4GStat
                {
                    TownId = townId,
                    StatTime = DateTime.Parse(statDate),
                    TotalMrs = totalMrs,
                    FirstNeighbors = firstNeighbors,
                    SecondNeighbors = secondNeighbors,
                    ThirdNeighbors = thirdNeighbors
                }
            });
        }

        public PreciseRegionDateView QueryLastDateStat(string initialDate, string city)
        {
            var service = new PreciseRegionStatService(_statRepository.Object, _townRepository.Object);
            return service.QueryLastDateStat(DateTime.Parse(initialDate), city);
        }
    }
}
