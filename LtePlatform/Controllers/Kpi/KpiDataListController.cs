using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Parameters.Abstract;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.DataService;

namespace LtePlatform.Controllers.Kpi
{
    public class KpiDataListController : ApiController
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICdmaRegionStatRepository _statRepository;

        public KpiDataListController(IRegionRepository regionRepository, 
            ICdmaRegionStatRepository statRepository)
        {
            _regionRepository = regionRepository;
            _statRepository = statRepository;
        }

        public CdmaRegionDateView GetDateView(string city, DateTime statDate)
        {
            var service = new CdmaRegionStatService(_regionRepository, _statRepository);
            return service.QueryLastDateStat(statDate, city);
        }

        public CdmaRegionStatDetails GetKpiDetails(string city, DateTime beginDate, DateTime endDate)
        {
            var service = new CdmaRegionStatService(_regionRepository, _statRepository);
            return service.QueryStatDetails(beginDate, endDate, city);
        }
    }
}
