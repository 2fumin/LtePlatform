using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Mr;

namespace Lte.Evaluations.DataService.Mr
{
    public class CellStasticService
    {
        private readonly ICellStasticRepository _repository;

        public CellStasticService(ICellStasticRepository repository)
        {
            _repository = repository;
        }

        public CellStasticView QueryDateSpanAverageStat(int eNodebId, short pci, DateTime begin, DateTime end)
        {
            var dateSpanStats = new List<CellStastic>();
            while (begin < end)
            {
                var oneDayStats = _repository.GetList(eNodebId, pci, begin);
                if (oneDayStats.Any())
                    dateSpanStats.Add(new CellStastic
                    {
                        Mod3Count = oneDayStats.Sum(x => x.Mod3Count),
                        Mod6Count = oneDayStats.Sum(x => x.Mod6Count),
                        MrCount = oneDayStats.Sum(x => x.MrCount),
                        OverCoverCount = oneDayStats.Sum(x => x.OverCoverCount),
                        PreciseCount = oneDayStats.Sum(x => x.PreciseCount),
                        WeakCoverCount = oneDayStats.Sum(x => x.WeakCoverCount)
                    });
                begin = begin.AddDays(1);
            }
            return dateSpanStats.Any() ? new CellStasticView
            {
                Mod3Count = dateSpanStats.Average(x => x.Mod3Count),
                Mod6Count = dateSpanStats.Average(x => x.Mod6Count),
                MrCount = dateSpanStats.Average(x => x.MrCount),
                OverCoverCount = dateSpanStats.Average(x => x.OverCoverCount),
                PreciseCount = dateSpanStats.Average(x => x.PreciseCount),
                WeakCoverCount = dateSpanStats.Average(x => x.WeakCoverCount)
            } : null;
        }
    }
}
