using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Entities;

namespace LtePlatform.Controllers.Kpi
{
    public class DumpFlowController : ApiController
    {
        private readonly FlowService _service;

        public DumpFlowController(FlowService service)
        {
            _service = service;
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