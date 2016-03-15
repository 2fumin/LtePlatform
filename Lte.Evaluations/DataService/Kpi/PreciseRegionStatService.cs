using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService.Kpi
{
    public class PreciseRegionStatService
    {
        private readonly ITownPreciseCoverage4GStatRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public PreciseRegionStatService(ITownPreciseCoverage4GStatRepository statRepository,
            ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public PreciseRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var beginDate = initialDate.AddDays(-100);
            var endDate = initialDate.AddDays(1);
            var query =
                _statRepository.GetAllList(beginDate, endDate);
            var result =
                (from q in query
                    join t in _townRepository.GetAll(city) on q.TownId equals t.Id
                    select q).ToList();
            if (result.Count == 0) return null;
            var maxDate = result.Max(x => x.StatTime);
            var townViews =
                result.Where(x => x.StatTime == maxDate)
                    .Select(x => TownPreciseView.ConstructView(x, _townRepository))
                    .ToList();
            return new PreciseRegionDateView
            {
                StatDate = maxDate,
                TownPreciseViews = townViews,
                DistrictPreciseViews = Merge(townViews)
            };
        }

        public IEnumerable<PreciseRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(begin, end);
            var result =
                (from q in query
                 join t in _townRepository.GetAll(city) on q.TownId equals t.Id
                 select q).ToList();
            var townViews = result.Select(x => TownPreciseView.ConstructView(x, _townRepository)).ToList();
            return from view in townViews
                   group view by view.StatTime into g
                   select new PreciseRegionDateView
                   {
                       StatDate = g.Key,
                       TownPreciseViews = g.Select(x => x),
                       DistrictPreciseViews = Merge(g.Select(x => x))
                   };
        }

        public static IEnumerable<DistrictPreciseView> Merge(IEnumerable<TownPreciseView> townPreciseViews)
        {
            var preciseViews = townPreciseViews as TownPreciseView[] ?? townPreciseViews.ToArray();
            if (!preciseViews.Any()) return null;
            var districts = preciseViews.Select(x => x.District).Distinct();
            var city = preciseViews.ElementAt(0).City;
            return districts.Select(district =>
            {
                var view =
                    DistrictPreciseView.ConstructView(preciseViews.Where(x => x.District == district).ArraySum());
                view.City = city;
                view.District = district;
                return view;
            }).ToList();
        } 
    }
}
