using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    public class DumpFlowController : ApiController
    {
        private readonly FlowService _service;

        public DumpFlowController(FlowService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获取给定日期范围内的历史流量记录数")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("历史流量记录数")]
        public async Task<IEnumerable<FlowHistory>> Get(DateTime begin, DateTime end)
        {
            return await _service.GetFlowHistories(begin, end);
        }
    }

    public class DumpHuaweiFlowController : ApiController
    {
        private readonly FlowService _service;

        public DumpHuaweiFlowController(FlowService service)
        {
            _service = service;
        }

        [HttpPut]
        public Task<bool> Put()
        {
            return _service.DumpOneHuaweiStat();
        }

        [HttpGet]
        public int Get()
        {
            return _service.FlowHuaweiCount;
        }

        [HttpGet]
        public FlowHuawei Get(int index)
        {
            return _service.QueryHuaweiStat(index);
        }

        [HttpDelete]
        public void Delete()
        {
            _service.ClearHuaweiStats();
        }
    }

    public class DumpZteFlowController : ApiController
    {
        private readonly FlowService _service;

        public DumpZteFlowController(FlowService service)
        {
            _service = service;
        }

        [HttpPut]
        public Task<bool> Put()
        {
            return _service.DumpOneZteStat();
        }

        [HttpGet]
        public int Get()
        {
            return _service.FlowZteCount;
        }

        [HttpDelete]
        public void Delete()
        {
            _service.ClearZteStats();
        }
    }
}