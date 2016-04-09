using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("导入告警信息处理器")]
    public class DumpAlarmController : ApiController
    {
        private readonly AlarmsService _service;

        public DumpAlarmController(AlarmsService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条告警信息")]
        [ApiResponse("导入结果")]
        public bool Put()
        {
            return _service.DumpOneStat();
        }

        [HttpGet]
        [ApiDoc("获取当前等待导入告警数")]
        [ApiResponse("当前等待导入告警数")]
        public int Get()
        {
            return _service.GetAlarmsToBeDump();
        }

        [HttpGet]
        public IEnumerable<AlarmStat> Get(int begin, int range)
        {
            return _service.GetAlarmsToBeDump(begin, range);
        }

        [HttpGet]
        [ApiDoc("获取给定日期范围内的历史告警记录数")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("历史告警记录数")]
        public IEnumerable<AlarmHistory> Get(DateTime begin, DateTime end)
        {
            return _service.GetAlarmHistories(begin, end);
        }

        [HttpDelete]
        [ApiDoc("清除已上传告警记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearAlarmStats();
        }
    }
}
