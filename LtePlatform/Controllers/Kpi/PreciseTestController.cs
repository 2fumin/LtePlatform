using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("精确覆盖率查询控制器")]
    public class PreciseTestController : ApiController
    {
        private readonly PreciseImportService _service;
        private readonly ITownRepository _townRepository;

        public PreciseTestController(PreciseImportService service, ITownRepository townRepository)
        {
            _service = service;
            _townRepository = townRepository;
        }

        [ApiDoc("查询目前所有镇区的精确覆盖率信息")]
        [ApiResponse("精确覆盖率总数")]
        public int Get()
        {
            return _service.GetTownStats(PreciseImportService.PreciseCoverage4Gs.ToList()).Count();
        }

        [ApiDoc("查询指定日期的精确覆盖率统计指标")]
        [ApiParameterDoc("statTime", "查询指定日期")]
        [ApiResponse("精确覆盖率统计指标")]
        public IEnumerable<TownPreciseCoverage4GStat> Get(DateTime statTime)
        {
            return _service.GetMergeStats(_service.GetTownStats(PreciseImportService.PreciseCoverage4Gs.ToList()),
                statTime);
        }

        [ApiDoc("查询指定日期的精确覆盖率镇区统计指标")]
        [ApiParameterDoc("statTime", "查询指定日期")]
        [ApiParameterDoc("stamp", "查询标志，为了区别以上指令")]
        [ApiResponse("镇区精确覆盖率统计指标")]
        public List<TownPreciseView> Get(DateTime statTime, int stamp)
        {
            return _service.GetMergeStats(_service.GetTownStats(PreciseImportService.PreciseCoverage4Gs.ToList()),
                statTime).Select(x => TownPreciseView.ConstructView(x, _townRepository)).ToList();
        }
    }
}
