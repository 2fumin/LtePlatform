using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.Kpi
{
    public class AlarmCountController : ApiController
    {
        private readonly AlarmsService _service;

        public AlarmCountController(AlarmsService service)
        {
            _service = service;
        }

        public Tuple<int, int> Get(int eNodebId, DateTime begin, DateTime end, int index)
        {
            return new Tuple<int, int>(index, _service.GetCounts(eNodebId, begin, end));
        }

        public IEnumerable<AlarmStat> Get()
        {
            return _service.QueryAlarmStats();
        }

        public bool Get(int tryDump)
        {
            if (tryDump > 0) return _service.DumpOneStat();
            return false;
        }
    }
}
