using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService
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
                _statRepository.GetByDateSpan(beginDate, endDate);
            var result =
                (from q in query
                    join t in _townRepository.GetAll().Where(x => x.CityName == city) on q.TownId equals t.Id
                    select q).ToList();
            if (result.Count == 0) return null;
            var maxDate = result.Max(x => x.StatTime);
            var townViews = result.Where(x => x.StatTime == maxDate).Select(x=>new TownPreciseView(x, _townRepository));
            return new PreciseRegionDateView
            {
                StatDate = maxDate.ToShortDateString(),
                TownPreciseViews = townViews,
                DistrictPreciseViews = Merge(townViews)
            };
        }

        public static IEnumerable<DistrictPreciseView> Merge(IEnumerable<TownPreciseView> townPreciseViews)
        {
            if (!townPreciseViews.Any()) return null;
            var districts = townPreciseViews.Select(x => x.District).Distinct();
            var city = townPreciseViews.ElementAt(0).City;
            return districts.Select(district =>
                new DistrictPreciseView(townPreciseViews.Where(x => x.District == district).ArraySum())
                {
                    City = city,
                    District = district
                }).ToList();
        } 
    }
}
