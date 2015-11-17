using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;

namespace LtePlatform.Controllers.Kpi
{
    public class PreciseRegionController : ApiController
    {
        private readonly PreciseRegionStatService _service;

        public PreciseRegionController(PreciseRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        public PreciseRegionDateView Get(string city, DateTime statDate)
        {
            return _service.QueryLastDateStat(statDate, city);
        }
    }
}
