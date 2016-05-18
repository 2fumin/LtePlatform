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
    [ApiControl("导入流量查询控制器")]
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
            return await _service.GetFlowHistories(begin.Date, end.Date);
        }
    }

    [ApiControl("导入华为流量控制器")]
    public class DumpHuaweiFlowController : ApiController
    {
        private readonly FlowService _service;

        public DumpHuaweiFlowController(FlowService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条华为流量")]
        [ApiResponse("是否已经成功导入")]
        public Task<bool> Put()
        {
            return _service.DumpOneHuaweiStat();
        }

        [HttpGet]
        [ApiDoc("获得当前服务器中待导入的华为流量统计记录数")]
        [ApiResponse("当前服务器中待导入的华为流量统计记录数")]
        public int Get()
        {
            return _service.FlowHuaweiCount;
        }

        [HttpGet]
        [ApiDoc("获得指定记录位置的待导入的华为流量统计记录")]
        [ApiParameterDoc("index", "指定记录位置")]
        [ApiResponse("指定记录位置的待导入的华为流量统计记录")]
        public FlowHuawei Get(int index)
        {
            return _service.QueryHuaweiStat(index);
        }

        [HttpDelete]
        [ApiDoc("清空待导入的华为流量统计记录")]
        public void Delete()
        {
            _service.ClearHuaweiStats();
        }
    }

    [ApiControl("导入中兴流量控制器")]
    public class DumpZteFlowController : ApiController
    {
        private readonly FlowService _service;

        public DumpZteFlowController(FlowService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条中兴流量")]
        [ApiResponse("是否已经成功导入")]
        public Task<bool> Put()
        {
            return _service.DumpOneZteStat();
        }

        [HttpGet]
        [ApiDoc("获得当前服务器中待导入的中兴流量统计记录数")]
        [ApiResponse("当前服务器中待导入的中兴流量统计记录数")]
        public int Get()
        {
            return _service.FlowZteCount;
        }

        [HttpDelete]
        [ApiDoc("清空待导入的中兴流量统计记录")]
        public void Delete()
        {
            _service.ClearZteStats();
        }
    }
}