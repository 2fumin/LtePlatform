using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class PreciseStatService
    {
        private readonly IPreciseCoverage4GRepository _repository;
        private readonly IENodebRepository _eNodebRepository;

        public PreciseStatService(IPreciseCoverage4GRepository repository,
            IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
        }

        public IEnumerable<Precise4GView> GetTopCountViews(DateTime begin, DateTime end, int topCount,
            OrderPreciseStatService.OrderPreciseStatPolicy policy)
        {
            if (topCount <= 0)
                return new List<Precise4GView>();
            var query =
                _repository.GetAll()
                    .Where(x => x.StatTime >= begin && x.StatTime < end)
                    .OrderByDescending(x => x.TotalMrs)
                    .Take(topCount * (end - begin).Days);
            var result =
                from q in query.AsEnumerable()
                group q by new
                {
                    q.CellId,
                    q.SectorId
                }
                    into g
                select new PreciseCoverage4G
                {
                    CellId = g.Key.CellId,
                    SectorId = g.Key.SectorId,
                    FirstNeighbors = g.Sum(q => q.FirstNeighbors),
                    SecondNeighbors = g.Sum(q => q.SecondNeighbors),
                    ThirdNeighbors = g.Sum(q => q.ThirdNeighbors),
                    TotalMrs = g.Sum(q => q.TotalMrs)
                };

            var orderResult = result.Order(policy, topCount);
            return orderResult.Select(x => Precise4GView.ConstructView(x, _eNodebRepository));
        }

        public IEnumerable<PreciseCoverage4G> GetOneWeekStats(int cellId, byte sectorId, DateTime date)
        {
            var begin = date.AddDays(-7);
            var end = date.AddDays(1);
            return GetTimeSpanStats(cellId, sectorId, begin, end);
        }

        public IEnumerable<PreciseCoverage4G> GetTimeSpanStats(int cellId, byte sectorId, DateTime begin, DateTime end)
        {
            return _repository.GetAllList(cellId, sectorId, begin, end).OrderBy(x => x.StatTime);
        }
    }
}
