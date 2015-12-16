using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.College;

namespace LtePlatform.Controllers.College
{
    public class CollegeAlarmController : ApiController
    {
        private readonly CollegeAlarmService _service;

        public CollegeAlarmController(CollegeAlarmService service)
        {
            _service = service;
        }

        [HttpGet]
        public Dictionary<string, IEnumerable<Tuple<string, int>>> Get(DateTime begin, DateTime end)
        {
            return _service.GetAlarmCounts(begin, end);
        }
    }
}
