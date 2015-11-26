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

        public int Get(int eNodebId, DateTime begin, DateTime end)
        {
            return _service.GetCounts(eNodebId, begin, end);
        }

        public IEnumerable<int> Get(IEnumerable<int> eNodebIds, DateTime begin, DateTime end)
        {
            return eNodebIds.Select(id => _service.GetCounts(id, begin, end));
        } 
    }
}
