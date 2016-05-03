using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("单小区精确覆盖率查询控制器")]
    public class PreciseStatController : ApiController
    {
        private readonly PreciseStatService _service;
        private readonly ENodebQueryService _eNodebQueryService;

        public PreciseStatController(PreciseStatService service, ENodebQueryService eNodebQueryService)
        {
            _service = service;
            _eNodebQueryService = eNodebQueryService;
        }

        [HttpGet]
        [ApiDoc("获得TOP精确覆盖率排序的标准")]
        [ApiResponse("精确TOP覆盖率排序的标准")]
        public IEnumerable<string> Get()
        {
            return OrderPreciseStatService.OrderSelectionList.Select(x => x.Key);
        }

        [HttpGet]
        [ApiDoc("指定日期范围、TOP个数和排序标准，获得TOP精确覆盖率TOP列表")]
        [ApiParameterDoc("begin","开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiParameterDoc("orderSelection", "排序标准")]
        [ApiResponse("TOP精确覆盖率TOP列表")]
        public IEnumerable<Precise4GView> Get(DateTime begin, DateTime end, int topCount, string orderSelection)
        {
            return _service.GetTopCountViews(begin, end, topCount, orderSelection.GetPrecisePolicy());
        }

        [HttpGet]
        [ApiDoc("指定日期范围、TOP个数和排序标准，获得TOP精确覆盖率TOP列表")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiParameterDoc("orderSelection", "排序标准")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("TOP精确覆盖率TOP列表")]
        public IEnumerable<Precise4GView> Get(DateTime begin, DateTime end, int topCount, string orderSelection, string city, string district)
        {
            var eNodebs = _eNodebQueryService.GetENodebsByDistrict(city, district);
            return _service.GetTopCountViews(begin, end, topCount, orderSelection.GetPrecisePolicy(), eNodebs);
        }

        [HttpGet]
        [ApiDoc("指定小区（基站）编号、扇区编号和日期，获得一周以内的精确覆盖率统计记录")]
        [ApiParameterDoc("cellId", "小区（基站）编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("一周以内的精确覆盖率聚合统计记录")]
        public Precise4GView Get(int cellId, byte sectorId)
        {
            return _service.GetOneWeekStats(cellId, sectorId, DateTime.Today);
        }

        [HttpGet]
        [ApiDoc("指定小区（基站）编号、扇区编号和日期范围，获得一周以内的精确覆盖率统计记录")]
        [ApiParameterDoc("cellId", "小区（基站）编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("一周以内的精确覆盖率统计记录")]
        public IEnumerable<PreciseCoverage4G> Get(int cellId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.GetTimeSpanStats(cellId, sectorId, begin, end);
        }
    }
}
