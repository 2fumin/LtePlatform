using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.MapperSerive.Kpi;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Kpi
{
    public class PreciseStatService
    {
        private readonly IPreciseCoverage4GRepository _repository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICellRepository _cellRepository;
        private readonly IInfrastructureRepository _infrastructureRepository;

        public static int TotalMrsThreshold { get; } = 3000;

        public PreciseStatService(IPreciseCoverage4GRepository repository, IENodebRepository eNodebRepository,
            ICellRepository cellRepository, IInfrastructureRepository infrastructureRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
            _cellRepository = cellRepository;
            _infrastructureRepository = infrastructureRepository;
        }

        public IEnumerable<Precise4GView> GetTopCountViews(DateTime begin, DateTime end, int topCount,
            OrderPreciseStatService.OrderPreciseStatPolicy policy)
        {
            if (topCount <= 0)
                return new List<Precise4GView>();
            var orderResult = GetTopCountStats(begin, end, topCount, policy);
            return orderResult.Select(x =>
            {
                var view = Precise4GView.ConstructView(x.PreciseCoverage4G, _eNodebRepository, _infrastructureRepository,
                    _cellRepository);
                view.TopDates = x.TopDates;
                return view;
            });
        }

        public IEnumerable<Precise4GView> GetTopCountViews(DateTime begin, DateTime end, int topCount,
            OrderPreciseStatService.OrderPreciseStatPolicy policy, IEnumerable<ENodeb> eNodebs)
        {
            if (topCount <= 0)
                return new List<Precise4GView>();
            var orderResult = GetTopCountStats(begin, end, topCount, policy, eNodebs);
            return orderResult.Select(x =>
            {
                var view = Precise4GView.ConstructView(x.PreciseCoverage4G, _eNodebRepository, _infrastructureRepository,
                    _cellRepository);
                view.TopDates = x.TopDates;
                return view;
            });
        }

        public List<TopPrecise4GContainer> GetTopCountStats(DateTime begin, DateTime end, int topCount,
            OrderPreciseStatService.OrderPreciseStatPolicy policy)
        {
            var query =
                _repository.GetAll()
                    .Where(x => x.StatTime >= begin && x.StatTime < end && x.TotalMrs > TotalMrsThreshold);
            var result =
                from q in query.AsEnumerable()
                group q by new
                {
                    q.CellId,
                    q.SectorId
                }
                into g
                select new TopPrecise4GContainer
                {
                    PreciseCoverage4G = new PreciseCoverage4G
                    {
                        CellId = g.Key.CellId,
                        SectorId = g.Key.SectorId,
                        FirstNeighbors = g.Sum(q => q.FirstNeighbors),
                        SecondNeighbors = g.Sum(q => q.SecondNeighbors),
                        ThirdNeighbors = g.Sum(q => q.ThirdNeighbors),
                        TotalMrs = g.Sum(q => q.TotalMrs)
                    },
                    TopDates = g.Count()
                };

            var orderResult = result.Order(policy, topCount);
            return orderResult;
        }

        public List<TopPrecise4GContainer> GetTopCountStats(DateTime begin, DateTime end, int topCount,
            OrderPreciseStatService.OrderPreciseStatPolicy policy, IEnumerable<ENodeb> eNodebs)
        {
            var query =
                _repository.GetAll()
                    .Where(x => x.StatTime >= begin && x.StatTime < end && x.TotalMrs > TotalMrsThreshold);
            var result =
                from q in query.AsEnumerable()
                group q by new
                {
                    q.CellId,
                    q.SectorId
                }
                into g
                select new TopPrecise4GContainer
                {
                    PreciseCoverage4G = new PreciseCoverage4G
                    {
                        CellId = g.Key.CellId,
                        SectorId = g.Key.SectorId,
                        FirstNeighbors = g.Sum(q => q.FirstNeighbors),
                        SecondNeighbors = g.Sum(q => q.SecondNeighbors),
                        ThirdNeighbors = g.Sum(q => q.ThirdNeighbors),
                        TotalMrs = g.Sum(q => q.TotalMrs)
                    },
                    TopDates = g.Count()
                };

            var districtResults = from r in result
                join e in eNodebs on r.PreciseCoverage4G.CellId equals e.ENodebId
                select r;

            var orderResult = districtResults.Order(policy, topCount);
            return orderResult;
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
