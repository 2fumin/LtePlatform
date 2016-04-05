using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.ViewModels;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("告警列表查询控制器")]
    public class AlarmsController : ApiController
    {
        private readonly AlarmsService _service;

        public AlarmsController(AlarmsService service)
        {
            _service = service;
        }

        [ApiDoc("查询指定基站和时间段内的告警列表视图")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("告警列表视图")]
        public IEnumerable<AlarmView> Get(int eNodebId, DateTime begin, DateTime end)
        {
            return _service.Get(eNodebId, begin, end);
        }

        [ApiDoc("查询指定基站和时间段内的告警列表视图")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("告警列表视图")]
        public IEnumerable<AlarmView> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.Get(eNodebId, sectorId, begin, end);
        }

        [HttpPost]
        public int Post(HuaweiLocalCellDef cellDef)
        {
            return _service.DumpHuaweiAlarmInfo(cellDef);
        }
    }
}
