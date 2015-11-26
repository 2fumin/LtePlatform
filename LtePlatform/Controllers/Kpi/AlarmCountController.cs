using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;

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
    }
}
