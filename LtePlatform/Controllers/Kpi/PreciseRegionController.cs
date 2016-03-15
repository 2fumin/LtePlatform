using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels.RegionKpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("区域精确覆盖率查询控制器")]
    public class PreciseRegionController : ApiController
    {
        private readonly PreciseRegionStatService _service;

        public PreciseRegionController(PreciseRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定城市单个日期的区域精确覆盖率")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("statDate", "日期")]
        [ApiResponse("区域精确覆盖率")]
        public PreciseRegionDateView Get(string city, DateTime statDate)
        {
            return _service.QueryLastDateStat(statDate, city);
        }

        [HttpGet]
        [ApiDoc("查询指定城市和时间段的区域精确覆盖率列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("区域精确覆盖率列表")]
        public IEnumerable<PreciseRegionDateView> Get(DateTime begin, DateTime end, string city)
        {
            return _service.QueryDateViews(begin, end, city);
        }
    }
}
