using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Kpi;
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

        public void ImportPreciseRecord(int townId, string[] statDates, int[] totalMrs, int[] firstNeighbors,
            int[] secondNeighbors, int[] thirdNeighbors)
        {
            var statList = statDates.Select((t, i) => new TownPreciseCoverage4GStat
            {
                TownId = townId,
                StatTime = DateTime.Parse(statDates[i]),
                TotalMrs = totalMrs[i],
                FirstNeighbors = firstNeighbors[i],
                SecondNeighbors = secondNeighbors[i],
                ThirdNeighbors = thirdNeighbors[i]
            }).ToList();
            _statRepository.MockPreciseRegionStats(statList);
        }

        public void ImportPreciseRecord(int[] townIds, string[] statDates, int[] totalMrs, int[] firstNeighbors,
            int[] secondNeighbors, int[] thirdNeighbors)
        {
            var statList = statDates.Select((t, i) => new TownPreciseCoverage4GStat
            {
                TownId = townIds[i],
                StatTime = DateTime.Parse(statDates[i]),
                TotalMrs = totalMrs[i],
                FirstNeighbors = firstNeighbors[i],
                SecondNeighbors = secondNeighbors[i],
                ThirdNeighbors = thirdNeighbors[i]
            }).ToList();
            _statRepository.MockPreciseRegionStats(statList);
        }

        public PreciseRegionDateView QueryLastDateStat(string initialDate, string city)
        {
            var service = new PreciseRegionStatService(_statRepository.Object, _townRepository.Object);
            return service.QueryLastDateStat(DateTime.Parse(initialDate), city);
        }
    }
}
