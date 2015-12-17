using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网告警查询控制器")]
    public class CollegeAlarmController : ApiController
    {
        private readonly CollegeAlarmService _service;

        public CollegeAlarmController(CollegeAlarmService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有校园的按基站统计的告警数记录")]
        [ApiParameterDoc("date", "指定日期")]
        [ApiParameterDoc("hour", "指定时间")]
        [ApiResponse("所有校园的按基站统计的告警数记录")]
        public Dictionary<string, IEnumerable<Tuple<string, int>>> Get(DateTime begin, DateTime end)
        {
            return _service.GetAlarmCounts(begin, end);
        }
    }
}
