using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<Precise4GView> GetTopCountViews(DateTime begin, DateTime end, int topCount)
        {
            var orderResult = _repository.GetTopCountStats(begin, end, topCount);
            return orderResult.Select(x => new Precise4GView(x, _eNodebRepository));
        }

        public IEnumerable<PreciseCoverage4G> GetOneWeekStats(int cellId, byte sectorId, DateTime date)
        {
            var begin = date.AddDays(-7);
            var end = date.AddDays(1);
            return _repository.GetAllList(cellId, sectorId, begin, end);
        }

        public IEnumerable<PreciseCoverage4G> GetTimeSpanStats(int cellId, byte sectorId, DateTime begin, DateTime end)
        {
            return _repository.GetAllList(cellId, sectorId, begin, end);
        }
    }
}
