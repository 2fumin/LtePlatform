using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Parameters.Abstract;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels.RegionKpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("传统指标查询控制器")]
    public class KpiDataListController : ApiController
    {
        private readonly CdmaRegionStatService _service;

        public KpiDataListController(CdmaRegionStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询不晚于指定日期的指定城市的单日指标")]
        [ApiParameterDoc("city", "指定城市")]
        [ApiParameterDoc("statDate", "指定日期")]
        [ApiResponse("单日分区指标和实际统计日期")]
        public async Task<CdmaRegionDateView> Get(string city, DateTime statDate)
        {
            return await _service.QueryLastDateStat(statDate, city);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定城市的详细指标")]
        [ApiParameterDoc("city", "指定城市")]
        [ApiParameterDoc("beginDate", "开始日期")]
        [ApiParameterDoc("endDate", "结束日期")]
        [ApiResponse("指定城市的详细指标")]
        public async Task<IHttpActionResult> Get(string city, DateTime beginDate, DateTime endDate)
        {
            var details = await _service.QueryStatDetails(beginDate, endDate, city);
            return details == null ? (IHttpActionResult)BadRequest("查询日期内的指标失败！") : Ok(details);
        }

        [HttpGet]
        [ApiDoc("查询可用的指标名称")]
        [ApiResponse("可用的指标名称")]
        public List<string> Get()
        {
            return CdmaRegionStatDetails.KpiOptions;
        } 
    }
}
