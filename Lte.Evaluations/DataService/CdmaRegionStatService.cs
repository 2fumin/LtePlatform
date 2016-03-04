using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService
{
    public class CdmaRegionStatService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICdmaRegionStatRepository _statRepository;

        public CdmaRegionStatService(IRegionRepository regionRepository, ICdmaRegionStatRepository statRepository)
        {
            _regionRepository = regionRepository;
            _statRepository = statRepository;
        }

        public async Task<CdmaRegionDateView> QueryLastDateStat(DateTime initialDate, string city)
        {
            var beginDate = initialDate.AddDays(-100);
            var endDate = initialDate.AddDays(1);
            var query = await _statRepository.GetAllListAsync(beginDate, endDate);
            var regions
                = (await _regionRepository.GetAllListAsync(city)).Select(x => x.Region).Distinct().OrderBy(x => x);
            var result = (from q in query
                join r in regions
                    on q.Region equals r
                select q).ToList();
            if (result.Count == 0) return null;
            var maxDate = result.Max(x => x.StatDate);
            var stats = result.Where(x => x.StatDate == maxDate).ToList();
            var cityStat = stats.ArraySum();
            cityStat.Region = city;
            stats.Add(cityStat);
            return new CdmaRegionDateView
            {
                StatDate = maxDate,
                StatViews = Mapper.Map<List<CdmaRegionStat>, List<CdmaRegionStatView>>(stats)
            };
        }

        public CdmaRegionStatTrend QueryStatTrend(DateTime begin, DateTime end, string city)
        {
            var endDate = end.AddDays(1);
            var query = _statRepository.GetAll().Where(x => x.StatDate >= begin && x.StatDate < endDate);
            var regions
                = _regionRepository.GetAllList().Where(x => x.City == city)
                .Select(x => x.Region).Distinct().OrderBy(x => x);
            var result = (from q in query
                          join r in regions
                              on q.Region equals r
                          select q).ToList();
            if (result.Count == 0) return null;
            var dates = result.Select(x => x.StatDate).Distinct().OrderBy(x => x);
            var regionList = result.Select(x => x.Region).Distinct().ToList();
            var viewList = GenerateViewList(result, dates, regionList);
            regionList.Add(city);
            return new CdmaRegionStatTrend
            {
                StatDates = dates.Select(x => x.ToShortDateString()),
                RegionList = regionList,
                ViewList = viewList
            };
        }

        public CdmaRegionStatDetails QueryStatDetails(DateTime begin, DateTime end, string city)
        {
            var trend = QueryStatTrend(begin, end, city);
            return trend == null ? null : new CdmaRegionStatDetails(trend);
        }

        public static List<IEnumerable<CdmaRegionStatView>> GenerateViewList(IEnumerable<CdmaRegionStat> statList,
            IEnumerable<DateTime> dates, IEnumerable<string> regionList)
        {
            var query= (from t in regionList
                let regionStats = statList.Where(x => x.Region == t)
                select (from d in dates
                    join stat in regionStats on d equals stat.StatDate into temp
                    from tt in temp.DefaultIfEmpty()
                    select tt ?? new CdmaRegionStat
                    {
                        Region = t, StatDate = d
                    })
                into stats
                select stats.ToList()).ToList();
            var cityStat = new List<CdmaRegionStat>();
            for (var i = 0; i < dates.Count(); i++)
                cityStat.Add(query.Select(x => x.ElementAt(i)).ArraySum());
            query.Add(cityStat);
            return query.Select(Mapper.Map<List<CdmaRegionStat>, IEnumerable<CdmaRegionStatView>>).ToList();
        }
    }
}
