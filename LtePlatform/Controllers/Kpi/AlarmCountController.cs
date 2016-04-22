using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("告警查询（测试）控制器")]
    public class AlarmCountController : ApiController
    {
        private readonly AlarmsService _service;

        public AlarmCountController(AlarmsService service)
        {
            _service = service;
        }

        [ApiDoc("查询指定基站及日期范围内的告警数")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("index", "索引号，传递参数用，无实际含义")]
        [ApiResponse("告警数")]
        public Tuple<int, int> Get(int eNodebId, DateTime begin, DateTime end, int index)
        {
            return new Tuple<int, int>(index, _service.GetCounts(eNodebId, begin, end));
        }

        [ApiDoc("查询所有告警列表")]
        [ApiResponse("所有告警列表")]
        public IEnumerable<AlarmStat> Get()
        {
            return _service.QueryAlarmStats();
        }

        [ApiDoc("尝试导入一条告警记录")]
        [ApiParameterDoc("tryDump", "标志参数，大于0时表示尝试导入")]
        [ApiResponse("导入是否成功")]
        public bool Get(int tryDump)
        {
            if (tryDump > 0) return _service.DumpOneStat();
            return false;
        }
    }
}
