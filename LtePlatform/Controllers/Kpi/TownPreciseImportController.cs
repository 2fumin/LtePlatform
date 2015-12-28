using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.RegionKpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入镇区精确覆盖率的控制器")]
    public class TownPreciseImportController : ApiController
    {
        private readonly PreciseImportService _service;

        public TownPreciseImportController(PreciseImportService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得等待导入数据库的记录")]
        [ApiResponse("等待导入数据库的记录")]
        public List<TownPreciseView> Get()
        {
            return PreciseImportService.TownPreciseViews;
        }

        [HttpPost]
        [ApiDoc("导入镇区精确覆盖率")]
        [ApiParameterDoc("container", "等待导入数据库的记录")]
        public void Post(TownPreciseViewContainer container)
        {
            _service.DumpTownStats(container);
            PreciseImportService.TownPreciseViews.Clear();
        }
    }
}
