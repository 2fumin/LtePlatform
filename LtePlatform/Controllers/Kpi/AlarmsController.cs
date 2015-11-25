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
    public class AlarmsController : ApiController
    {
        private readonly AlarmsService _service;

        public AlarmsController(AlarmsService service)
        {
            _service = service;
        }

        public IEnumerable<AlarmView> Get(int eNodebId, DateTime begin, DateTime end)
        {
            return _service.Get(eNodebId, begin, end);
        }
    }
}
