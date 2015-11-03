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
    public class CdmaRegionStatService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICdmaRegionStatRepository _statRepository;

        public CdmaRegionStatService(IRegionRepository regionRepository, ICdmaRegionStatRepository statRepository)
        {
            _regionRepository = regionRepository;
            _statRepository = statRepository;
        }

        public CdmaRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            DateTime beginDate = initialDate.AddDays(-100);
            var query =
                _statRepository.GetAll()
                    .Where(x => x.StatDate >= beginDate && x.StatDate < initialDate.AddDays(1));
            var result = (from q in query
                join r in _regionRepository.GetAll().Where(x => x.City == city)
                    on q.Region equals r.Region
                select q).ToList();
            if (result.Count == 0) return null;
            DateTime maxDate = result.Max(x => x.StatDate);
            var stats = result.Where(x => x.StatDate == maxDate).ToList();
            var cityStat = stats.ArraySum();
            cityStat.Region = city;
            stats.Add(cityStat);
            return new CdmaRegionDateView
            {
                StatDate = maxDate.ToShortDateString(),
                StatViews = stats.Select(x => new CdmaRegionStatView(x))
            };
        }
    }
}
