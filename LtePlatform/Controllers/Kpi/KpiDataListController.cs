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
        private readonly CdmaRegionStatService _service;

        public KpiDataListController(CdmaRegionStatService service)
        {
            _service = service;
        }

        public CdmaRegionDateView GetDateView(string city, DateTime statDate)
        {
            return _service.QueryLastDateStat(statDate, city);
        }

        public CdmaRegionStatDetails GetKpiDetails(string city, DateTime beginDate, DateTime endDate)
        {
            return _service.QueryStatDetails(beginDate, endDate, city);
        }
    }
}
