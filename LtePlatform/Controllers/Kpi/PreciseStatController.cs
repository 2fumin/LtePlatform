using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.Kpi
{
    public class PreciseStatController : ApiController
    {
        private readonly PreciseStatService _service;

        public PreciseStatController(PreciseStatService service)
        {
            _service = service;
        }

        public IEnumerable<Precise4GView> Get(DateTime begin, DateTime end, int topCount, byte fieldSelector)
        {
            return _service.GetTopCountViews(begin, end, topCount, fieldSelector);
        }

        public IEnumerable<PreciseCoverage4G> Get(int cellId, byte sectorId, DateTime date)
        {
            return _service.GetOneWeekStats(cellId, sectorId, date);
        }

        public IEnumerable<PreciseCoverage4G> Get(int cellId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.GetTimeSpanStats(cellId, sectorId, begin, end);
        }
    }
}
