using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels.Precise;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("邻区监控查询操作控制器")]
    public class NeighborMonitorController : ApiController
    {
        private readonly NeighborMonitorService _service;

        public NeighborMonitorController(NeighborMonitorService service)
        {
            _service = service;
        }

        [HttpPost]
        [ApiDoc("将小区信息加入邻区监控列表")]
        [ApiParameterDoc("view", "小区信息")]
        [ApiResponse("更新结果")]
        public int Post(Precise4GView view)
        {
            return _service.AddOneMonitor(view.CellId, view.SectorId);
        }
    }
}
